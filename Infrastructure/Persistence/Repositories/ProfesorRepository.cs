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
        public Profesor? GetByEmail(string email)
        {
            return _context.Profesores.FirstOrDefault(p => p.Email == email);
        }

        
    }
}
