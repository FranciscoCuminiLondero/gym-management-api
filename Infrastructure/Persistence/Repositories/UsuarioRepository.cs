using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly GymDbContext _context;
        public UsuarioRepository(GymDbContext context) : base(context) 
        { 
            _context = context;
        }

        public Usuario? GetByEmail(string email)
         => _context.Usuarios.FirstOrDefault(u => u.Email == email);

        public bool ExistsByEmail(string email)
            => _context.Usuarios.Any(u => u.Email == email);

        public T? GetById<T>(int id) where T : Usuario
            => _context.Usuarios.OfType<T>().FirstOrDefault(u => u.Id == id);
    }
}
