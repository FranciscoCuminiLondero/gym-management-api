using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IPlanRepository _planRepository;

        public AlumnoService(IAlumnoRepository alumnoRepository, IPlanRepository planRepository)
        {
            _alumnoRepository = alumnoRepository;
            _planRepository = planRepository;
        }

        public AlumnoResponse? CreateAlumno(CreateAlumnoRequest request)
        {
            if (_alumnoRepository.ExistsByEmail(request.Email))
                return null;

            if (!_planRepository.IsActivo(request.PlanId))
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
                PlanId = request.PlanId,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            if (!_alumnoRepository.Create(alumno))
                return null;

            return MapToResponse(alumno);
        }

        public AlumnoResponse? GetPerfilCompleto(int id)
        {
            var alumno = _alumnoRepository.GetAlumnoCompleto(id);
            return alumno != null ? MapToCompleteResponse(alumno) : null;
        }

        public AlumnoResponse? GetAlumnoById(int id)
        {
            var alumno = _alumnoRepository.GetById(id);
            return alumno != null ? MapToResponse(alumno) : null;
        }

        public List<AlumnoResponse> GetAlumnosActivos()
        {
            var alumnos = _alumnoRepository.GetAll().Where(a => a.Activo).ToList();
            return alumnos.Select(MapToResponse).ToList();
        }

        public bool UpdateAlumno(int id, CreateAlumnoRequest request)
        {
            var alumno = _alumnoRepository.GetById(id);
            if (alumno == null) return false;

            alumno.Nombre = request.Nombre;
            alumno.Apellido = request.Apellido;
            alumno.Dni = request.Dni;
            alumno.Email = request.Email;
            alumno.Telefono = request.Telefono;
            alumno.FechaNacimiento = request.FechaNacimiento;
            alumno.PlanId = request.PlanId;

            return _alumnoRepository.Update(alumno);
        }

        public bool DeleteAlumno(int id)
        {
            var alumno = _alumnoRepository.GetById(id);
            if (alumno == null) return false;

            alumno.Activo = false;
            return _alumnoRepository.Update(alumno);
        }

        private AlumnoResponse MapToResponse(Alumno alumno)
        {
            return new AlumnoResponse
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                Dni = alumno.Dni,
                Email = alumno.Email,
                Telefono = alumno.Telefono,
                FechaNacimiento = alumno.FechaNacimiento,
                Activo = alumno.Activo,
                NombreCompleto = alumno.NombreCompleto,
                TipoUsuario = alumno.GetTipoUsuario(),
                PlanId = alumno.PlanId,
                TieneMembresiaActiva = alumno.TieneMembresiaActiva()
            };
        }

        private AlumnoResponse MapToCompleteResponse(Alumno alumno)
        {
            var response = MapToResponse(alumno);
            response.PlanNombre = alumno.Plan?.Nombre ?? "";
            // Agregar mapeo de membresías y reservas si es necesario
            return response;
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}