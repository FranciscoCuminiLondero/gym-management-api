using Application.Abstractions;
using Contract.Responses;

namespace Application.Services
{
    public class ClaseService : IClaseService
    {
        private readonly IClaseRepository _claseRepository;
        private readonly IReservaRepository _reservaRepository;
        public ClaseService(IClaseRepository claseRepository, IReservaRepository reservaRepository)
        {
            _claseRepository = claseRepository;
            _reservaRepository = reservaRepository;
        }

        public List<ClaseResponse> GetDisponiblesPorFecha(DateOnly fecha)
        {
            var clases = _claseRepository.GetDisponiblesPorFecha(fecha);
            return clases.Select(c => new ClaseResponse
            {
                Id = c.Id,
                ProfesorId = c.ProfesorId,
                SalaId = c.SalaId,
                SucursalId = c.SucursalId,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Duracion = c.DuracionMinutos,
                HoraInicio = c.HoraInicio,
                Fecha = c.Fecha,
                Capacidad = c.Capacidad,
                CupoDisponible = c.Capacidad - _reservaRepository.GetByClaseId(c.Id).Count,
                Activa = c.Activa
            }).ToList();
        }

        public ClaseResponse? GetById(int id)
        {
            var clase = _claseRepository.GetById(id);
            if (clase == null) return null;

            return new ClaseResponse
            {
                Id = clase.Id,
                ProfesorId = clase.ProfesorId,
                SalaId = clase.SalaId,
                SucursalId = clase.SucursalId,
                Nombre = clase.Nombre,
                Descripcion = clase.Descripcion,
                Duracion = clase.DuracionMinutos,
                HoraInicio = clase.HoraInicio,
                Fecha = clase.Fecha,
                Capacidad = clase.Capacidad,
                CupoDisponible = clase.Capacidad - _reservaRepository.GetByClaseId(clase.Id).Count,
                Activa = clase.Activa
            };
        }
    }
}
