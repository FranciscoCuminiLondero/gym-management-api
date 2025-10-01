using Application.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IProfesorRepository _profesorRepository;
        private readonly IClaseRepository _claseRepository;

        public NotificacionService(
            INotificacionRepository notificacionRepository, 
            IAlumnoRepository alumnoRepository,
            IProfesorRepository profesorRepository,
            IClaseRepository claseRepository)
        {
            _notificacionRepository = notificacionRepository;
            _alumnoRepository = alumnoRepository;
            _profesorRepository = profesorRepository;
            _claseRepository = claseRepository;
        }

        public void NotificarReservaConfirmada(int alumnoId, int claseId)
        {
            var alumno = _alumnoRepository.GetById(alumnoId);
            var clase = _claseRepository.GetById(claseId);

            if (alumno == null || clase == null) return;

            var mensaje = $"Tu reserva para la clase '{clase.Nombre}' el {clase.Fecha:yyyy-MM-dd} ha sido confirmada.";
            var notificacion = new Notificacion
            {
                Destino = alumno.Email,
                Mensaje = mensaje,
                FechaEnvio = DateOnly.FromDateTime(DateTime.Today),
                Enviado = true,
            };

            _notificacionRepository.Create(notificacion);
        }

        public void NotificarNuevaReservaAlProfesor(int profesorId, int alumnoId, int claseId)
        {
            var clase = _claseRepository.GetById(claseId);
            if (clase == null) return;

            var profesor = _profesorRepository.GetById(profesorId);
            if (profesor == null) return;

            var mensaje = $"El alumno con ID {alumnoId} se ha reservado tu clase '{clase.Nombre}' el {clase.Fecha:yyyy-MM-dd}.";
            var notificacion = new Notificacion
            {
                Destino = profesor.Email,
                Mensaje = mensaje,
                FechaEnvio = DateOnly.FromDateTime(DateTime.Today),
                Enviado = true,
            };

            _notificacionRepository.Create(notificacion);
        }
    }
}
