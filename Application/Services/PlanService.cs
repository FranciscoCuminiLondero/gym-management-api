using Application.Abstractions;
using Contract.Responses;

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
    }
}
