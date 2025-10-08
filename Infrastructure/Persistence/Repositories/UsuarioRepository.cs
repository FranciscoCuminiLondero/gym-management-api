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

        public Contract.Responses.UsuarioResponse? GetDtoByEmail(string email)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Email == email);
            if (alumno != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = alumno.Id, Nombre = alumno.Nombre, Email = alumno.Email, Role = alumno.Role };
            }

            var profesor = _context.Profesores.FirstOrDefault(p => p.Email == email);
            if (profesor != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = profesor.Id, Nombre = profesor.Nombre, Email = profesor.Email, Role = profesor.Role };
            }

            return null;
        }

        public Contract.Responses.UsuarioResponse? GetDtoById(int id)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Id == id);
            if (alumno != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = alumno.Id, Nombre = alumno.Nombre, Email = alumno.Email, Role = alumno.Role };
            }

            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = profesor.Id, Nombre = profesor.Nombre, Email = profesor.Email, Role = profesor.Role };
            }

            return null;
        }

        public Domain.Entities.Usuario? GetWithPasswordByEmail(string email)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Email == email);
            if (alumno != null) return alumno;

            var profesor = _context.Profesores.FirstOrDefault(p => p.Email == email);
            if (profesor != null) return profesor;

            return null;
        }

        public bool IsActivo(int id)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Id == id);
            if (alumno != null) return alumno.Activo;

            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor != null) return profesor.Activo;

            return false;
        }
    }
}
