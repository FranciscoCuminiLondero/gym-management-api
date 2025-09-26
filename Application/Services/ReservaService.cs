using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IClaseRepository _claseRepository;
        public ReservaService(
            IReservaRepository reservaRepository,
            IAlumnoRepository alumnoRepository,
            IClaseRepository claseRepository)
        {
            _reservaRepository = reservaRepository;
            _alumnoRepository = alumnoRepository;
            _claseRepository = claseRepository;
        }

        public bool Create(CreateReservaRequest request)
        {
            if (request == null || request.AlumnoId <= 0 || request.ClaseId <= 0)
                return false;

            if (!_alumnoRepository.IsActivo(request.AlumnoId))
                return false;

            if (!_alumnoRepository.HasMembresiaActiva(request.AlumnoId))
                return false;

            // 3. Validar que la clase exista, esté activa y no haya pasado
            var clase = _claseRepository.GetById(request.ClaseId);
            if (clase == null || !clase.Activa || clase.Fecha < DateOnly.FromDateTime(DateTime.Today))
                return false;

            if (_reservaRepository.ExistsByAlumnoAndClase(request.AlumnoId, request.ClaseId))
                return false;

            var reservasActuales = _reservaRepository.GetByClaseId(clase.Id).Count;
            if (reservasActuales >= clase.Capacidad)
                return false;

            var nuevaReserva = new Reserva
            {
                AlumnoId = request.AlumnoId,
                ClaseId = request.ClaseId,
                FechaReserva = DateOnly.FromDateTime(DateTime.Today),
                Activo = true
            };

            return _reservaRepository.Create(nuevaReserva);
        }

        public List<ReservaResponse> GetByAlumnoId(int alumnoId)
        {
            var reservas = _reservaRepository.GetByAlumnoId(alumnoId);
            return reservas.Select(reserva => new ReservaResponse
            {
                Id = reserva.Id,
                AlumnoId = reserva.AlumnoId,
                ClaseId = reserva.ClaseId,
                FechaReserva = reserva.FechaReserva,
                Activo = reserva.Activo
            }).ToList();
        }

        public List<ReservaResponse> GetByClaseId(int claseId)
        {
            var reservas = _reservaRepository.GetByClaseId(claseId);
            return reservas.Select(reserva => new ReservaResponse
            {
                Id = reserva.Id,
                AlumnoId = reserva.AlumnoId,
                ClaseId = reserva.ClaseId,
                FechaReserva = reserva.FechaReserva,
                Activo = reserva.Activo
            }).ToList();
        }
    }
}
