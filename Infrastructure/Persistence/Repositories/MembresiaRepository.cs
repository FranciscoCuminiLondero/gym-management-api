using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class MembresiaRepository : BaseRepository<Membresia>, IMembresiaRepository
    {
        private readonly GymDbContext _context;
        public MembresiaRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
        public bool TieneMembresiaActiva(int alumnoId)
        {
            return _context.Membresias.Any(m =>
                m.AlumnoId == alumnoId &&
                m.Activa &&
                m.FechaFin >= DateOnly.FromDateTime(DateTime.Today));
        }
    }
}
