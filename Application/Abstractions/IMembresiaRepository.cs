using Domain.Entities;

namespace Application.Abstractions
{
    public interface IMembresiaRepository : IBaseRepository<Membresia>
    {
        bool TieneMembresiaActiva(int alumnoId);
    }
}
