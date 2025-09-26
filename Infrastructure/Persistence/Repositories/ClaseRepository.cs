using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ClaseRepository : BaseRepository<Clase>, IClaseRepository
    {
        private readonly GymDbContext _context;
        public ClaseRepository(GymDbContext context) : base(context) 
        { 
            _context = context;
        }
        public Clase? GetByIdWithDetails(int id)
        {
            return _context.Clases.FirstOrDefault(c => c.Id == id);
        }
        public List<Clase> GetByProfesorId(int profesorId)
        {
            return _context.Clases
                .Where(c => c.ProfesorId == profesorId && c.Activa)
                .ToList();
        }

        public List<Clase> GetBySucursalId(int sucursalId)
        {
            return _context.Clases
                .Where(c => c.SucursalId == sucursalId && c.Activa)
                .ToList();
        }

        public List<Clase> GetBySalaId(int salaId)
        {
            return _context.Clases
                .Where(c => c.SalaId == salaId && c.Activa)
                .ToList();
        }

        public List<Clase> GetDisponiblesPorFecha(DateOnly fecha)
        {
            return _context.Clases
                .Where(c => c.Fecha == fecha && c.Activa)
                .ToList();
        }
    }
}
