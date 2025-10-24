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
            return profesores.Select(p => new ProfesorResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Dni = p.Dni,
                Email = p.Email,
                Telefono = p.Telefono,
                Activo = p.Activo
            }).ToList();
        }

        public ProfesorResponse? GetById(int id)
        {
            var profesor = _profesorRepository.GetById(id);
            if (profesor == null) return null;

            return new ProfesorResponse
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                Dni = profesor.Dni,
                Email = profesor.Email,
                Telefono = profesor.Telefono,
                Activo = profesor.Activo
            };
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

            return _profesorRepository.Update(profesor);
        }
    }
}
