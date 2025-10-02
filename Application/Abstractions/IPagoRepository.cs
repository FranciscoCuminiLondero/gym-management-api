using Domain.Entities;

namespace Application.Abstractions
{
    public interface IPagoRepository : IBaseRepository<Pago>
    {
        List<Pago> GetByMembresiaId(int membresiaId);
        bool TienePagoPorMembresiaYFecha(int membresiaId, DateOnly fechaPago);
    }
}
