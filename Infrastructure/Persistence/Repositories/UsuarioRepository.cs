using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GymDbContext _context;

        public UsuarioRepository(GymDbContext context)
        {
            _context = context;
        }

        public Usuario? GetByEmail(string email)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Email == email);
            if (alumno != null) return alumno;

            var profesor = _context.Profesores.FirstOrDefault(p => p.Email == email);
            if (profesor != null) return profesor;

            return null;
        }

        public Usuario? GetById(int id)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Id == id);
            if (alumno != null) return alumno;

            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor != null) return profesor;

            return null;
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Alumnos.Any(a => a.Email == email) || _context.Profesores.Any(p => p.Email == email);
        }
    }
}
