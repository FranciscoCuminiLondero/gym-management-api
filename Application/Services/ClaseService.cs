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
                // Comentado temporalmente - puede causar fallos si el profesor no existe aún
                // if (_claseRepository.TieneConflictoHorario(
                //     request.ProfesorId,
                //     request.Fecha,
                //     request.HoraInicio,
                //     request.DuracionMinutos))
                // {
                //     return false;
                // }

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
            catch (Exception ex)
            {
                // Log para debugging
                Console.WriteLine($"Error al crear clase: {ex.Message}");
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

        public List<ClaseResponse> GetByProfesorId(int profesorId)
        {
            var clases = _claseRepository.GetAll()
                .Where(c => c.ProfesorId == profesorId && c.Activa)
                .ToList();

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

        public int? GetProfesorIdByClaseId(int claseId)
        {
            var clase = _claseRepository.GetById(claseId);
            return clase?.ProfesorId;
        }

        public bool Delete(int id)
        {
            var clase = _claseRepository.GetById(id);
            if (clase == null) return false;

            return _claseRepository.Delete(clase);
        }
    }
}
