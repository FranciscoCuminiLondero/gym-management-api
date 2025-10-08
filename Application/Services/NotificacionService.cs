using Application.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClaseRepository _claseRepository;

        public NotificacionService(
            INotificacionRepository notificacionRepository,
            IUsuarioRepository usuarioRepository,
            IClaseRepository claseRepository)
        {
            _notificacionRepository = notificacionRepository;
            _usuarioRepository = usuarioRepository;
            _claseRepository = claseRepository;
        }

        public void NotificarReservaConfirmada(int alumnoId, int claseId)
        {
            var alumnoDto = _usuarioRepository.GetDtoById(alumnoId);
            var clase = _claseRepository.GetById(claseId);

            if (alumnoDto == null || clase == null) return;

            var mensaje = $"Tu reserva para la clase '{clase.Nombre}' el {clase.Fecha:yyyy-MM-dd} ha sido confirmada.";
            var notificacion = new Notificacion
            {
                Destino = alumnoDto.Email,
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

            var profesorDto = _usuarioRepository.GetDtoById(profesorId);
            if (profesorDto == null) return;

            var mensaje = $"El alumno reservó la clase {clase.Nombre} el {clase.Fecha:yyyy-MM-dd}";

            var notificacion = new Notificacion
            {
                Destino = profesorDto.Email,
                Mensaje = mensaje,
                FechaEnvio = DateOnly.FromDateTime(DateTime.Today),
                Enviado = true,
            };

            _notificacionRepository.Create(notificacion);
        }
    }
}
