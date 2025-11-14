using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _profesorRepository;
        private readonly IUsuarioService _usuarioService;

        public ProfesorService(IProfesorRepository profesorRepository, IUsuarioService usuarioService)
        {
            _profesorRepository = profesorRepository;
            _usuarioService = usuarioService;
        }

        public List<ProfesorResponse> GetAll()
        {
            var profesores = _profesorRepository.GetAll();
            return profesores.Select(MapToProfesorResponse).ToList();
        }

        public List<ProfesorResponse> GetBySucursalId(int sucursalId)
        {
            var profesores = _profesorRepository.GetAll()
                .Where(p => p.SucursalId == sucursalId)
                .ToList();
            return profesores.Select(MapToProfesorResponse).ToList();
        }

        public ProfesorResponse? GetById(int id)
        {
            var profesor = _profesorRepository.GetById(id);
            if (profesor == null) return null;

            return MapToProfesorResponse(profesor);
        }

        public bool Create(CreateProfesorRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Nombre) ||
                string.IsNullOrWhiteSpace(request.Apellido) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return false;
            }

            if (_usuarioService.ExistsByEmail(request.Email))
            {
                return false;
            }

            var nuevoProfesor = new Profesor
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Dni = request.Dni,
                Email = request.Email,
                Telefono = request.Telefono,
                Activo = true
            };

            return _profesorRepository.Create(nuevoProfesor);
        }

        public bool Update(int id, UpdateProfesorRequest request)
        {
            var profesor = _profesorRepository.GetById(id);
            if (profesor == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre))
                profesor.Nombre = request.Nombre;

            if (!string.IsNullOrWhiteSpace(request.Apellido))
                profesor.Apellido = request.Apellido;

            if (!string.IsNullOrWhiteSpace(request.Especialidad))
                profesor.Especialidad = request.Especialidad;

            if (!string.IsNullOrWhiteSpace(request.Telefono))
                profesor.Telefono = request.Telefono;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (request.Email != profesor.Email && _usuarioService.ExistsByEmail(request.Email))
                    return false;
                profesor.Email = request.Email;
            }

            if (request.FechaNacimiento.HasValue)
                profesor.FechaNacimiento = request.FechaNacimiento.Value;

            if (request.SucursalId.HasValue)
                profesor.SucursalId = request.SucursalId.Value;

            return _profesorRepository.Update(profesor);
        }

        public bool Delete(int id)
        {
            var profesor = _profesorRepository.GetById(id);
            if (profesor == null) return false;

            // Desactivar en lugar de eliminar
            profesor.Activo = false;
            return _profesorRepository.Update(profesor);
        }

        private ProfesorResponse MapToProfesorResponse(Profesor profesor)
        {
            return new ProfesorResponse
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                Especialidad = profesor.Especialidad,
                Dni = profesor.Dni,
                Email = profesor.Email,
                Telefono = profesor.Telefono,
                FechaNacimiento = profesor.FechaNacimiento,
                SucursalId = profesor.SucursalId,
                Activo = profesor.Activo
            };
        }
    }
}
