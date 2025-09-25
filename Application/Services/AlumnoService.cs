using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;
        public AlumnoService(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
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

            if (_alumnoRepository.ExistsByDni(request.Dni))
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
    }
}
