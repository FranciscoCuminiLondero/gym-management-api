using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        private readonly GymDbContext _context;

        public PlanRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsActivo(int planId)
        {
            var plan = _context.Planes.FirstOrDefault(p => p.Id == planId);
            return plan != null && plan.Activo;
        }


    }
}
