using Domain.Entities;

namespace Application.Abstractions
{
    public interface IPlanRepository : IBaseRepository<Plan>
    {
        bool IsActivo(int planId);
    }
}
