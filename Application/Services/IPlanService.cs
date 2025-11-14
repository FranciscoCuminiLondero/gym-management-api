using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IPlanService
    {
        List<PlanResponse> GetPlanesActivos();
        PlanResponse? GetById(int id);
        bool Create(CreatePlanRequest request);
        bool Update(int id, UpdatePlanRequest request);
        bool Delete(int id);
    }
}
