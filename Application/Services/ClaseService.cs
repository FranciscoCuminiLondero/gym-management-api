using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

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

        public bool Create(CreateClaseRequest request)
        {
            try
            {
                var clase = new Clase
                {
                    ProfesorId = request.ProfesorId,
                    SalaId = request.SalaId,
                    SucursalId = request.SucursalId,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    DuracionMinutos = request.DuracionMinutos,
                    HoraInicio = request.HoraInicio,
                    Fecha = request.Fecha,
                    Capacidad = request.Capacidad,
                    Activa = true
                };

                return _claseRepository.Create(clase);
            }
            catch
            {
                return false;
            }
        }

        public List<ClaseResponse> GetAll()
        {
            var clases = _claseRepository.GetAll().Where(c => c.Activa).ToList();
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

        public bool Delete(int id)
        {
            var clase = _claseRepository.GetById(id);
            if (clase == null) return false;

            return _claseRepository.Delete(clase);
        }
    }
}
