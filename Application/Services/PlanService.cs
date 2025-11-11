using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

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
            return planes.Select(p => new PlanResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                DuracionDias = p.DuracionDias
            }).ToList();
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
                Activo = true
            };

            return _planRepository.Create(plan);
        }

        public bool Delete(int id)
        {
            var plan = _planRepository.GetById(id);
            if (plan == null) return false;

            return _planRepository.Delete(plan);
        }
    }
}
