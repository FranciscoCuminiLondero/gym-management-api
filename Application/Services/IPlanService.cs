using Contract.Responses;

namespace Application.Services
{
    public interface IPlanService
    {
        List<PlanResponse> GetPlanesActivos();
    }
}
