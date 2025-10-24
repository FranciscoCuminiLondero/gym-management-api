using Domain.Entities;

namespace Application.Abstractions
{
    public interface ISucursalRepository : IBaseRepository<Sucursal>
    {
        List<Sucursal> GetActivas();
    }
}
