using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using System.Security.Cryptography;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IProfesorRepository _profesorRepository;
        private readonly IMembresiaService _membresiaService;
        private readonly IPlanRepository _planRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(
            IAlumnoRepository alumnoRepository,
            IProfesorRepository profesorRepository,
            IMembresiaService membresiaService,
            IPlanRepository planRepository,
            IUsuarioService usuarioService,
            IUsuarioRepository usuarioRepository)
        {
            _alumnoRepository = alumnoRepository;
            _profesorRepository = profesorRepository;
            _membresiaService = membresiaService;
            _planRepository = planRepository;
            _usuarioService = usuarioService;
            _usuarioRepository = usuarioRepository;
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
            var usuario = _usuarioService.GetWithPasswordByEmail(request.Email);
            if (usuario == null)
                return null;

            // Verificar si la cuenta está bloqueada
            if (usuario.LockoutEnd.HasValue && usuario.LockoutEnd.Value > DateTime.UtcNow)
            {
                // Usuario bloqueado temporalmente
                return null;
            }

            // Verificar contraseña
            if (VerifyPassword(request.Password, usuario.PasswordHash))
            {
                // Login exitoso: resetear intentos fallidos
                if (usuario.FailedLoginAttempts > 0 || usuario.LockoutEnd.HasValue)
                {
                    usuario.FailedLoginAttempts = 0;
                    usuario.LockoutEnd = null;
                    _usuarioRepository.Update(usuario);
                }

                return new AuthResponse
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Role = usuario.Role
                };
            }

            // Contraseña incorrecta: incrementar intentos fallidos
            usuario.FailedLoginAttempts++;

            // Bloquear cuenta si alcanza 3 intentos fallidos
            if (usuario.FailedLoginAttempts >= 3)
            {
                // Bloquear por 15 minutos
                usuario.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
            }

            _usuarioRepository.Update(usuario);

            return null;
        }

        private string HashPassword(string password)
        {
            // Usar PBKDF2 con salt para mayor seguridad
            // Genera un salt aleatorio de 16 bytes
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Genera el hash usando PBKDF2 con 10000 iteraciones
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // Combina salt + hash para almacenar
            byte[] hashBytes = new byte[48]; // 16 bytes salt + 32 bytes hash
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            // Extrae salt y hash del string almacenado
            byte[] hashBytes = Convert.FromBase64String(hash);

            // Extrae el salt (primeros 16 bytes)
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Genera hash de la contraseña ingresada con el mismo salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] testHash = pbkdf2.GetBytes(32);

            // Compara los hashes
            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != testHash[i])
                    return false;
            }

            return true;
        }
    }
}
