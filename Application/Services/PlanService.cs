using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using System.Text.Json;

namespace Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public List<PlanResponse> GetPlanesActivos()
        {
            var planes = _planRepository.GetByCriterial(p => p.Activo);
            return planes.Select(MapToPlanResponse).ToList();
        }

        public PlanResponse? GetById(int id)
        {
            var plan = _planRepository.GetById(id);
            if (plan == null) return null;

            return MapToPlanResponse(plan);
        }

        public bool Create(CreatePlanRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || request.Precio <= 0 || request.DuracionDias <= 0)
                return false;

            var plan = new Plan
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                DuracionDias = request.DuracionDias,
                MaxReservasPorMes = request.MaxReservasPorMes,
                TiposPermitidos = request.TiposPermitidos != null && request.TiposPermitidos.Any()
                    ? JsonSerializer.Serialize(request.TiposPermitidos)
                    : null,
                Activo = true
            };

            return _planRepository.Create(plan);
        }

        public bool Update(int id, UpdatePlanRequest request)
        {
            var plan = _planRepository.GetById(id);
            if (plan == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre))
                plan.Nombre = request.Nombre;

            if (!string.IsNullOrWhiteSpace(request.Descripcion))
                plan.Descripcion = request.Descripcion;

            if (request.Precio.HasValue && request.Precio.Value > 0)
                plan.Precio = request.Precio.Value;

            if (request.DuracionDias.HasValue && request.DuracionDias.Value > 0)
                plan.DuracionDias = request.DuracionDias.Value;

            if (request.MaxReservasPorMes.HasValue)
                plan.MaxReservasPorMes = request.MaxReservasPorMes.Value;

            if (request.TiposPermitidos != null)
            {
                plan.TiposPermitidos = request.TiposPermitidos.Any()
                    ? JsonSerializer.Serialize(request.TiposPermitidos)
                    : null;
            }

            return _planRepository.Update(plan);
        }

        public bool Delete(int id)
        {
            var plan = _planRepository.GetById(id);
            if (plan == null) return false;

            return _planRepository.Delete(plan);
        }

        private PlanResponse MapToPlanResponse(Plan plan)
        {
            List<string> tiposPermitidos = new List<string>();
            if (!string.IsNullOrWhiteSpace(plan.TiposPermitidos))
            {
                try
                {
                    var tipos = JsonSerializer.Deserialize<List<string>>(plan.TiposPermitidos);
                    if (tipos != null)
                        tiposPermitidos = tipos;
                }
                catch
                {
                    // Si falla la deserialización, retornar lista vacía
                }
            }

            return new PlanResponse
            {
                Id = plan.Id,
                Nombre = plan.Nombre,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                DuracionMeses = plan.DuracionDias / 30, // Convertir días a meses aproximados
                Estado = plan.Activo ? "activo" : "inactivo",
                MaxReservasPorMes = plan.MaxReservasPorMes,
                TiposPermitidos = tiposPermitidos
            };
        }
    }
}
