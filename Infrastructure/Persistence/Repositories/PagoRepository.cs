using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class PagoRepository : BaseRepository<Pago>, IPagoRepository
    {
        private readonly GymDbContext _context;

        public PagoRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Pago> GetByMembresiaId(int membresiaId)
        {
            return _context.Pagos
                .Where(p => p.MembresiaId == membresiaId)
                .ToList();
        }

        public bool TienePagoPorMembresiaYFecha(int membresiaId, DateOnly fechaPago)
        {
            return _context.Pagos.Any(p =>
                p.MembresiaId == membresiaId &&
                p.FechaPago == fechaPago);
        }

    }
}
