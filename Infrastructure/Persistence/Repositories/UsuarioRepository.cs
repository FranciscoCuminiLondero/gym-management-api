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
            var alumno = _context.Usuarios.OfType<Alumno>().FirstOrDefault(a => a.Email == email);
            if (alumno != null) return alumno;

            var profesor = _context.Usuarios.OfType<Profesor>().FirstOrDefault(p => p.Email == email);
            if (profesor != null) return profesor;

            return null;
        }

        public Usuario? GetById(int id)
        {
            var alumno = _context.Usuarios.OfType<Alumno>().FirstOrDefault(a => a.Id == id);
            if (alumno != null) return alumno;

            var profesor = _context.Usuarios.OfType<Profesor>().FirstOrDefault(p => p.Id == id);
            if (profesor != null) return profesor;

            return null;
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Usuarios.OfType<Alumno>().Any(a => a.Email == email) || _context.Usuarios.OfType<Profesor>().Any(p => p.Email == email);
        }

        public bool ExistsByDni(string dni)
        {
            return _context.Usuarios.OfType<Alumno>().Any(a => a.Dni == dni);
        }

        public Contract.Responses.UsuarioResponse? GetDtoByEmail(string email)
        {
            var alumno = _context.Usuarios.OfType<Alumno>().FirstOrDefault(a => a.Email == email);
            if (alumno != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = alumno.Id, Nombre = alumno.Nombre, Email = alumno.Email, Role = alumno.Role };
            }

            var profesor = _context.Usuarios.OfType<Profesor>().FirstOrDefault(p => p.Email == email);
            if (profesor != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = profesor.Id, Nombre = profesor.Nombre, Email = profesor.Email, Role = profesor.Role };
            }

            return null;
        }

        public Contract.Responses.UsuarioResponse? GetDtoById(int id)
        {
            var alumno = _context.Usuarios.OfType<Alumno>().FirstOrDefault(a => a.Id == id);
            if (alumno != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = alumno.Id, Nombre = alumno.Nombre, Email = alumno.Email, Role = alumno.Role };
            }

            var profesor = _context.Usuarios.OfType<Profesor>().FirstOrDefault(p => p.Id == id);
            if (profesor != null)
            {
                return new Contract.Responses.UsuarioResponse { Id = profesor.Id, Nombre = profesor.Nombre, Email = profesor.Email, Role = profesor.Role };
            }

            return null;
        }

        public List<Contract.Responses.UsuarioResponse> GetAllDtos()
        {
            var alumnos = _context.Usuarios.OfType<Alumno>().Select(a => new Contract.Responses.UsuarioResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Email = a.Email,
                Role = a.Role
            });

            var profesores = _context.Usuarios.OfType<Profesor>().Select(p => new Contract.Responses.UsuarioResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Email = p.Email,
                Role = p.Role
            });

            return alumnos.Concat(profesores).ToList();
        }

        public (List<Contract.Responses.UsuarioResponse> Items, int Total) GetPagedDtos(int page, int pageSize, string? q = null)
        {
            var alumnoQuery = _context.Usuarios.OfType<Alumno>().AsQueryable();
            var profesorQuery = _context.Usuarios.OfType<Profesor>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var qLc = q.ToLower();
                alumnoQuery = alumnoQuery.Where(a => (a.Nombre ?? "").ToLower().Contains(qLc) || (a.Email ?? "").ToLower().Contains(qLc));
                profesorQuery = profesorQuery.Where(p => (p.Nombre ?? "").ToLower().Contains(qLc) || (p.Email ?? "").ToLower().Contains(qLc));
            }

            var alumnosProjected = alumnoQuery.Select(a => new Contract.Responses.UsuarioResponse
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Email = a.Email,
                Role = a.Role
            });

            var profesoresProjected = profesorQuery.Select(p => new Contract.Responses.UsuarioResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Email = p.Email,
                Role = p.Role
            });

            var union = alumnosProjected.Concat(profesoresProjected);

            var total = union.Count();

            var items = union
                .OrderBy(u => u.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (items, total);
        }

        public Domain.Entities.Usuario? GetWithPasswordByEmail(string email)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario != null) return usuario;

            return null;
        }

        public bool IsActivo(int id)
        {
            var alumno = _context.Usuarios.OfType<Alumno>().FirstOrDefault(a => a.Id == id);
            if (alumno != null) return alumno.Activo;

            var profesor = _context.Usuarios.OfType<Profesor>().FirstOrDefault(p => p.Id == id);
            if (profesor != null) return profesor.Activo;

            return false;
        }

        public bool HasMembresiaActiva(int alumnoId)
        {
            return _context.Membresias.Any(m =>
                m.AlumnoId == alumnoId &&
                m.Activa &&
                m.FechaFin >= DateOnly.FromDateTime(DateTime.Today));
        }
    }
}
