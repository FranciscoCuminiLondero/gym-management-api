using Domain.Entities;
using Application.Abstractions;

namespace Infrastructure.Persistence.Repositories
{
    public class AlumnoRepository : BaseRepository<Alumno>, IAlumnoRepository
    {
        private readonly GymDbContext _context;
        public AlumnoRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public bool ExistsByDni(string dni)
        {
            return _context.Alumnos.Any(a => a.Dni == dni);
        }
    }
}
