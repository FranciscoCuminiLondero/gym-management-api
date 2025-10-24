using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class SucursalRepository : BaseRepository<Sucursal>, ISucursalRepository
    {
        public SucursalRepository(GymDbContext context) : base(context)
        {
        }

        public List<Sucursal> GetActivas()
        {
            return GetByCriterial(s => s.Activa);
        }
    }
}
