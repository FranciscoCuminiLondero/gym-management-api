using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class SalaRepository : BaseRepository<Sala>, ISalaRepository
    {
        public SalaRepository(GymDbContext context) : base(context)
        {
        }

        public List<Sala> GetBySucursalId(int sucursalId)
        {
            return GetByCriterial(s => s.SucursalId == sucursalId);
        }
    }
}
