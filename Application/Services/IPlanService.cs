using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IPlanService
    {
        List<PlanResponse> GetPlanesActivos();
        bool Create(CreatePlanRequest request);
    }
}
