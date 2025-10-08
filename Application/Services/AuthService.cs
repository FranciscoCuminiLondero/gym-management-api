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
        private readonly IPlanRepository _plan_repository;
        private readonly IUsuarioService _usuarioService;
        private readonly string _jwtKey = "tu_clave_secreta_muy_larga_y_segura_32_caracteres";

        public AuthService(
            IAlumnoRepository alumnoRepository,
            IProfesorRepository profesorRepository,
            IMembresiaService membresiaService,
            IPlanRepository planRepository,
            IUsuarioService usuarioService)
        {
            _alumnoRepository = alumnoRepository;
            _profesor_repository = profesorRepository;
            _membresiaService = membresiaService;
            _plan_repository = planRepository;
            _usuarioService = usuarioService;
        }

        public AuthResponse? Register(RegisterRequest request)
        {
            if (request.Role == "Alumno")
            {
                if (!_planRepository.IsActivo(request.PlanId))
                    return null;

                if (_usuarioService.ExistsByEmail(request.Email))
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
                };
            }
            else if (request.Role == "Profesor")
            {
                if (_usuarioService.ExistsByEmail(request.Email))
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
                };
            }

            return null;
        }

        public AuthResponse? Login(LoginRequest request)
        {
            var usuario = _usuarioService.GetByEmail(request.Email);
            if (usuario != null && VerifyPassword(request.Password, usuario.PasswordHash))
            {
                return new AuthResponse
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Role = usuario.Role,
                    Token = GenerateToken(usuario.Email, usuario.Role, usuario.Id)
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
