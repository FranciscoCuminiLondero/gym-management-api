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
            if (request.Role == "Alumno")
            {
                if (!_planRepository.IsActivo(request.PlanId))
                    return null;

                if (_alumnoRepository.GetByCriterial(a => a.Email == request.Email).Any())
                    return null;

                var alumno = new Alumno
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Dni = request.Dni,
                    Email = request.Email,
                    Telefono = request.Telefono,
                    FechaNacimiento = request.FechaNacimiento,
                    Activo = true,
                    PasswordHash = HashPassword(request.Password),
                    Role = "Alumno"
                };

                if (!_alumnoRepository.Create(alumno)) return null;

                var membresiaRequest = new CreateMembresiaRequest
                {
                    AlumnoId = alumno.Id,
                    PlanId = request.PlanId
                };

                if (!_membresiaService.AsociarMembresia(membresiaRequest))
                    return null;

                return new AuthResponse
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    Role = alumno.Role,
                    Token = GenerateToken(alumno.Email, alumno.Role, alumno.Id)
                };
            }
            else if (request.Role == "Profesor")
            {
                if (_profesorRepository.GetByCriterial(p => p.Email == request.Email).Any())
                    return null;

                var profesor = new Profesor
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Dni = request.Dni,
                    Email = request.Email,
                    Telefono = request.Telefono,
                    Activo = true,
                    PasswordHash = HashPassword(request.Password),
                    Role = "Profesor"
                };

                if (!_profesorRepository.Create(profesor)) return null;

                return new AuthResponse
                {
                    Id = profesor.Id,
                    Nombre = profesor.Nombre,
                    Role = profesor.Role,
                    Token = GenerateToken(profesor.Email, profesor.Role, profesor.Id)
                };
            }

            return null;
        }

        public AuthResponse? Login(LoginRequest request)
        {
            var alumno = _alumnoRepository.GetByCriterial(a => a.Email == request.Email).FirstOrDefault();
            if (alumno != null && VerifyPassword(request.Password, alumno.PasswordHash))
            {
                return new AuthResponse
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    Role = alumno.Role,
                    Token = GenerateToken(alumno.Email, alumno.Role, alumno.Id)
                };
            }

            // Buscar en Profesores
            var profesor = _profesorRepository.GetByCriterial(p => p.Email == request.Email).FirstOrDefault();
            if (profesor != null && VerifyPassword(request.Password, profesor.PasswordHash))
            {
                return new AuthResponse
                {
                    Id = profesor.Id,
                    Nombre = profesor.Nombre,
                    Role = profesor.Role,
                    Token = GenerateToken(profesor.Email, profesor.Role, profesor.Id)
                };
            }

            return null;
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
            // "token simulado"
            return $"fake-jwt-token-for-{email}-{role}-{id}";
        }
    }
}
