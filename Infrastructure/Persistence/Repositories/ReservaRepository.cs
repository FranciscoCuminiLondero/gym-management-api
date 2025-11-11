using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        private readonly GymDbContext _context;
        public ReservaRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
        public bool ExistsByAlumnoAndClase(int alumnoId, int claseId)
        {
            return _context.Reservas.Any(r =>
                r.AlumnoId == alumnoId &&
                r.ClaseId == claseId &&
                r.Activo);
        }

        public bool ExistsByAlumnoAndFecha(int alumnoId, DateOnly fecha)
        {
            return _context.Reservas.Any(r =>
                r.AlumnoId == alumnoId &&
                r.Clase.Fecha == fecha &&
                r.Activo);
        }

        public List<Reserva> GetByAlumnoId(int alumnoId)
        {
            return _context.Reservas
                .Where(r => r.AlumnoId == alumnoId && r.Activo)
                .ToList();
        }

        public List<Reserva> GetByClaseId(int claseId)
        {
            return _context.Reservas
                .Where(r => r.ClaseId == claseId && r.Activo)
                .ToList();
        }
    }
}
