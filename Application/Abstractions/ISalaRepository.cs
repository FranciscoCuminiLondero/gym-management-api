using Domain.Entities;

namespace Application.Abstractions
{
    public interface ISalaRepository : IBaseRepository<Sala>
    {
        List<Sala> GetBySucursalId(int sucursalId);
    }
}
