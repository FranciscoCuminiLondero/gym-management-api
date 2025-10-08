using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IProfesorRepository _profesorRepository;
        private readonly IMembresiaService _membresiaService;
        private readonly IPlanRepository _planRepository;
        private readonly string _jwtKey = "tu_clave_secreta_muy_larga_y_segura_32_caracteres";

        public AuthService(
            IAlumnoRepository alumnoRepository,
            IProfesorRepository profesorRepository,
            IMembresiaService membresiaService,
            IPlanRepository planRepository)
        {
            _alumnoRepository = alumnoRepository;
            _profesorRepository = profesorRepository;
            _membresiaService = membresiaService;
            _planRepository = planRepository;
        }

        public AuthResponse? Register(RegisterRequest request)
        {
            // Verificar si el email ya existe en cualquiera de los repositorios
            bool emailExists = request.Role switch
            {
                "Alumno" => _alumnoRepository.ExistsByEmail(request.Email),
                "Profesor" => _profesorRepository.ExistsByEmail(request.Email),
                _ => throw new ArgumentException("Rol no válido")
            };

            if (emailExists) return null;

            return request.Role switch
            {
                "Alumno" => RegisterAlumno(request),
                "Profesor" => RegisterProfesor(request),
                _ => throw new ArgumentException("Rol no válido")
            };
        }

        private AuthResponse? RegisterAlumno(RegisterRequest request)
        {
            if (!request.PlanId.HasValue || !_planRepository.IsActivo(request.PlanId.Value))
                return null;

            var alumno = new Alumno
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Dni = request.Dni,
                Email = request.Email,
                Telefono = request.Telefono,
                FechaNacimiento = request.FechaNacimiento,
                PasswordHash = HashPassword(request.Password),
                PlanId = request.PlanId.Value,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            if (!_alumnoRepository.Create(alumno))
                return null;

            // Asociar membresía
            var membresiaRequest = new CreateMembresiaRequest
            {
                AlumnoId = alumno.Id,
                PlanId = request.PlanId.Value
            };

            if (!_membresiaService.AsociarMembresia(membresiaRequest))
                return null;

            return new AuthResponse
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Role = "Alumno"
            };
        }

        private AuthResponse? RegisterProfesor(RegisterRequest request)
        {
            var profesor = new Profesor
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Dni = request.Dni,
                Email = request.Email,
                Telefono = request.Telefono,
                FechaNacimiento = request.FechaNacimiento,
                PasswordHash = HashPassword(request.Password),
                Especialidad = request.Especialidad,
                FechaContratacion = DateTime.UtcNow,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            if (!_profesorRepository.Create(profesor))
                return null;

            return new AuthResponse
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Role = "Profesor"
            };
        }

        public AuthResponse? Login(LoginRequest request)
        {
            // Buscar en ambos repositorios
            var alumno = _alumnoRepository.GetByEmail(request.Email);
            if (alumno != null && VerifyPassword(request.Password, alumno.PasswordHash))
            {
                return new AuthResponse
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    Role = "Alumno",
                    Token = GenerateToken(alumno.Email, "Alumno", alumno.Id)
                };
            }

            var profesor = _profesorRepository.GetByEmail(request.Email);
            if (profesor != null && VerifyPassword(request.Password, profesor.PasswordHash))
            {
                return new AuthResponse
                {
                    Id = profesor.Id,
                    Nombre = profesor.Nombre,
                    Role = "Profesor",
                    Token = GenerateToken(profesor.Email, "Profesor", profesor.Id)
                };
            }

            return null; // Usuario no encontrado o contraseña incorrecta
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }

        private string GenerateToken(string email, string role, int id)
        {
            // ⚠️ En tu AuthController, este método se reemplazará por JWT real
            // Por ahora, lo dejamos para que el servicio no dependa de IConfiguration
            return $"fake-jwt-token-for-{email}-{role}-{id}";
        }
    }
}
