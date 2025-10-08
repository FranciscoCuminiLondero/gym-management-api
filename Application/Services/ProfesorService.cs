using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public interface IProfesorService
    {
        ProfesorResponse? CreateProfesor(CreateProfesorRequest request);
        ProfesorResponse? GetProfesorById(int id);
        List<ProfesorResponse> GetProfesoresActivos();
        bool UpdateProfesor(int id, CreateProfesorRequest request);
        bool DeleteProfesor(int id);
        ProfesorResponse? GetPerfilCompleto(int id);
        List<ProfesorResponse> GetProfesoresPorEspecialidad(string especialidad);
    }

    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _profesorRepository;

        public ProfesorService(IProfesorRepository profesorRepository)
        {
            _profesorRepository = profesorRepository;
        }

        public ProfesorResponse? CreateProfesor(CreateProfesorRequest request)
        {
            if (_profesorRepository.ExistsByEmail(request.Email))
                return null;

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
                FechaContratacion = request.FechaContratacion ?? DateTime.UtcNow,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            if (!_profesorRepository.Create(profesor))
                return null;

            return MapToResponse(profesor);
        }

        public ProfesorResponse? GetPerfilCompleto(int id)
        {
            var profesor = _profesorRepository.GetProfesorCompleto(id);
            return profesor != null ? MapToCompleteResponse(profesor) : null;
        }

        public ProfesorResponse? GetProfesorById(int id)
        {
            var profesor = _profesorRepository.GetById(id);
            return profesor != null ? MapToResponse(profesor) : null;
        }

        public List<ProfesorResponse> GetProfesoresActivos()
        {
            var profesores = _profesorRepository.GetAll().Where(p => p.Activo).ToList();
            return profesores.Select(MapToResponse).ToList();
        }

        public List<ProfesorResponse> GetProfesoresPorEspecialidad(string especialidad)
        {
            var profesores = _profesorRepository.GetProfesoresPorEspecialidad(especialidad);
            return profesores.Select(MapToResponse).ToList();
        }

        public bool UpdateProfesor(int id, CreateProfesorRequest request)
        {
            var profesor = _profesorRepository.GetById(id);
            if (profesor == null) return false;

            profesor.Nombre = request.Nombre;
            profesor.Apellido = request.Apellido;
            profesor.Dni = request.Dni;
            profesor.Email = request.Email;
            profesor.Telefono = request.Telefono;
            profesor.FechaNacimiento = request.FechaNacimiento;
            profesor.Especialidad = request.Especialidad;
            profesor.FechaContratacion = request.FechaContratacion;

            return _profesorRepository.Update(profesor);
        }

        public bool DeleteProfesor(int id)
        {
            var profesor = _profesorRepository.GetById(id);
            if (profesor == null) return false;

            profesor.Activo = false;
            return _profesorRepository.Update(profesor);
        }

        private ProfesorResponse MapToResponse(Profesor profesor)
        {
            return new ProfesorResponse
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                Dni = profesor.Dni,
                Email = profesor.Email,
                Telefono = profesor.Telefono,
                FechaNacimiento = profesor.FechaNacimiento,
                Activo = profesor.Activo,
                NombreCompleto = profesor.NombreCompleto,
                TipoUsuario = profesor.GetTipoUsuario(),
                Especialidad = profesor.Especialidad,
                FechaContratacion = profesor.FechaContratacion,
                TotalClasesAsignadas = profesor.Clases.Count
            };
        }

        private ProfesorResponse MapToCompleteResponse(Profesor profesor)
        {
            var response = MapToResponse(profesor);
            // Agregar mapeo de clases activas si es necesario
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