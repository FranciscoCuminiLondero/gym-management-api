using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IUsuarioService _usuarioService;
        public AlumnoService(IAlumnoRepository alumnoRepository, IUsuarioService usuarioService)
        {
            _alumnoRepository = alumnoRepository;
            _usuarioService = usuarioService;
        }

        public List<AlumnoResponse> GetAll()
        {
            var alumnos = _alumnoRepository.GetAll();
            return alumnos.Select(a => new AlumnoResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido,
                Dni = a.Dni,
                Email = a.Email,
                Telefono = a.Telefono,
                FechaNacimiento = a.FechaNacimiento,
                Activo = a.Activo
            }).ToList();
        }

        public AlumnoResponse? GetById(int id)
        {
            var alumno = _alumnoRepository.GetById(id);
            if (alumno == null) return null;

            return new AlumnoResponse
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                Dni = alumno.Dni,
                Email = alumno.Email,
                Telefono = alumno.Telefono,
                FechaNacimiento = alumno.FechaNacimiento,
                Activo = alumno.Activo
            };
        }

        public bool Create(CreateAlumnoRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Nombre) ||
                string.IsNullOrWhiteSpace(request.Apellido) ||
                string.IsNullOrWhiteSpace(request.Dni) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return false;
            }

            if (_usuarioService.ExistsByDni(request.Dni))
            {
                return false;
            }

            var nuevoAlumno = new Alumno
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Dni = request.Dni,
                Email = request.Email,
                Telefono = request.Telefono,
                FechaNacimiento = request.FechaNacimiento,
                Activo = true
            };

            return _alumnoRepository.Create(nuevoAlumno);
        }

        public bool Update(int id, UpdateAlumnoRequest request)
        {
            var alumno = _alumnoRepository.GetById(id);
            if (alumno == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre))
                alumno.Nombre = request.Nombre;

            if (!string.IsNullOrWhiteSpace(request.Apellido))
                alumno.Apellido = request.Apellido;

            if (!string.IsNullOrWhiteSpace(request.Telefono))
                alumno.Telefono = request.Telefono;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (request.Email != alumno.Email && _usuarioService.ExistsByEmail(request.Email))
                    return false;
                alumno.Email = request.Email;
            }

            if (request.FechaNacimiento.HasValue)
                alumno.FechaNacimiento = request.FechaNacimiento.Value;

            return _alumnoRepository.Update(alumno);
        }
    }
}
