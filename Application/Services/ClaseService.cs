using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using System.Text.Json;

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
                if (_claseRepository.TieneConflictoHorario(
                    request.ProfesorId,
                    request.Fecha,
                    request.HoraInicio,
                    request.DuracionMinutos))
                {
                    return false;
                }

                var clase = new Clase
                {
                    ProfesorId = request.ProfesorId,
                    SalaId = request.SalaId,
                    SucursalId = request.SucursalId,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    Imagen = request.Imagen,
                    Tipo = request.Tipo,
                    DuracionMinutos = request.DuracionMinutos,
                    HoraInicio = request.HoraInicio,
                    Fecha = request.Fecha,
                    Dias = request.Dias != null && request.Dias.Any()
                        ? JsonSerializer.Serialize(request.Dias)
                        : null,
                    Capacidad = request.Capacidad,
                    MostrarEnHome = request.MostrarEnHome,
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
            return clases.Select(MapToClaseResponse).ToList();
        }

        public List<ClaseResponse> GetDisponiblesPorFecha(DateOnly fecha)
        {
            var clases = _claseRepository.GetDisponiblesPorFecha(fecha);
            return clases.Select(MapToClaseResponse).ToList();
        }

        public List<ClaseResponse> GetByProfesorId(int profesorId)
        {
            var clases = _claseRepository.GetAll()
                .Where(c => c.ProfesorId == profesorId && c.Activa)
                .ToList();

            return clases.Select(MapToClaseResponse).ToList();
        }

        public List<ClaseResponse> GetBySucursalId(int sucursalId)
        {
            var clases = _claseRepository.GetAll()
                .Where(c => c.SucursalId == sucursalId && c.Activa)
                .ToList();

            return clases.Select(MapToClaseResponse).ToList();
        }

        public ClaseResponse? GetById(int id)
        {
            var clase = _claseRepository.GetById(id);
            if (clase == null) return null;

            return MapToClaseResponse(clase);
        }

        public int? GetProfesorIdByClaseId(int claseId)
        {
            var clase = _claseRepository.GetById(claseId);
            return clase?.ProfesorId;
        }

        public bool Update(int id, UpdateClaseRequest request)
        {
            var clase = _claseRepository.GetById(id);
            if (clase == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre))
                clase.Nombre = request.Nombre;

            if (!string.IsNullOrWhiteSpace(request.Descripcion))
                clase.Descripcion = request.Descripcion;

            if (!string.IsNullOrWhiteSpace(request.Imagen))
                clase.Imagen = request.Imagen;

            if (!string.IsNullOrWhiteSpace(request.Tipo))
                clase.Tipo = request.Tipo;

            if (request.DuracionMinutos.HasValue && request.DuracionMinutos.Value > 0)
                clase.DuracionMinutos = request.DuracionMinutos.Value;

            if (request.HoraInicio.HasValue)
                clase.HoraInicio = request.HoraInicio.Value;

            if (request.Fecha.HasValue)
                clase.Fecha = request.Fecha.Value;

            if (request.Dias != null)
            {
                clase.Dias = request.Dias.Any()
                    ? JsonSerializer.Serialize(request.Dias)
                    : null;
            }

            if (request.Capacidad.HasValue && request.Capacidad.Value > 0)
                clase.Capacidad = request.Capacidad.Value;

            if (request.MostrarEnHome.HasValue)
                clase.MostrarEnHome = request.MostrarEnHome.Value;

            return _claseRepository.Update(clase);
        }

        public bool Delete(int id)
        {
            var clase = _claseRepository.GetById(id);
            if (clase == null) return false;

            return _claseRepository.Delete(clase);
        }

        private ClaseResponse MapToClaseResponse(Clase clase)
        {
            var cuposReservados = _reservaRepository.GetByClaseId(clase.Id).Count;
            var cupoDisponible = clase.Capacidad - cuposReservados;

            // Deserializar días
            List<string> dias = new List<string>();
            if (!string.IsNullOrWhiteSpace(clase.Dias))
            {
                try
                {
                    var diasDeserializados = JsonSerializer.Deserialize<List<string>>(clase.Dias);
                    if (diasDeserializados != null)
                        dias = diasDeserializados;
                }
                catch
                {
                    // Si falla, retornar lista vacía
                }
            }

            // Calcular horarios ISO
            var fechaBase = clase.Fecha.ToDateTime(TimeOnly.MinValue);
            var horarioInicio = new DateTime(
                fechaBase.Year,
                fechaBase.Month,
                fechaBase.Day,
                clase.HoraInicio.Hour,
                clase.HoraInicio.Minute,
                0
            );
            var horarioFin = horarioInicio.AddMinutes(clase.DuracionMinutos);

            return new ClaseResponse
            {
                Id = clase.Id,
                ProfesorId = clase.ProfesorId,
                SalaId = clase.SalaId,
                SucursalId = clase.SucursalId,
                Nombre = clase.Nombre,
                Descripcion = clase.Descripcion,
                Imagen = clase.Imagen,
                CupoMaximo = clase.Capacidad,
                Tipo = clase.Tipo,
                HorarioInicio = horarioInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                HorarioFin = horarioFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                Dias = dias,
                MostrarEnHome = clase.MostrarEnHome,
                CuposActuales = cuposReservados,
                CupoDisponible = cupoDisponible,
                Activa = clase.Activa,
                // Campos heredados para compatibilidad
                Duracion = clase.DuracionMinutos,
                HoraInicio = clase.HoraInicio,
                Fecha = clase.Fecha,
                Capacidad = clase.Capacidad
            };
        }
    }
}
