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

        public bool ExistsByEmail(string email)
        {
            return _context.Alumnos.Any(a => a.Email == email);
        }

        public bool HasMembresiaActiva(int alumnoId)
        {
            return _context.Membresias.Any(m =>
                m.AlumnoId == alumnoId &&
                m.Activa &&
                m.FechaFin >= DateOnly.FromDateTime(DateTime.Today));
        }

        public bool IsActivo(int alumnoId)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Id == alumnoId);
            return alumno != null && alumno.Activo;
        }

        public Alumno? GetByIdWithMembresias(int id)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Id == id);
            if (alumno == null) return null;

            var membresias = _context.Membresias
                .Where(m => m.AlumnoId == id)
                .ToList();

            return alumno;
        }
    }
}
