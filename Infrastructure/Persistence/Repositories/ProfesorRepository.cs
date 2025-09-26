using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ProfesorRepository : BaseRepository<Profesor>, IProfesorRepository
    {
        private readonly GymDbContext _context;
        public ProfesorRepository(GymDbContext context) : base(context) 
        { 
            _context = context;
        }
        public bool ExistsByEmail(string email)
        {
            return _context.Profesores.Any(p => p.Email == email);
        }

        public Profesor? GetByEmail(string email)
        {
            return _context.Profesores.FirstOrDefault(p => p.Email == email);
        }

        public bool IsActivo(int profesorId)
        {
            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == profesorId);
            return profesor != null && profesor.Activo;
        }

    }
}
