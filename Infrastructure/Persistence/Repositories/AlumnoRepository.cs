using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AlumnoRepository : BaseRepository<Alumno>, IAlumnoRepository
    {
        private readonly GymDbContext _context;

        public AlumnoRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public Alumno? GetByEmail(string email)
            => _context.Alumnos.FirstOrDefault(a => a.Email == email);

        public bool ExistsByEmail(string email)
            => _context.Alumnos.Any(a => a.Email == email);

        public List<Alumno> GetAlumnosConMembresiaActiva()
        {
            return _context.Alumnos
                .Include(a => a.Membresias)
                .Where(a => a.Activo &&
                       a.Membresias.Any(m => m.Activa && m.FechaFin > DateOnly.FromDateTime(DateTime.Now)))
                .ToList();
        }

        public Alumno? GetAlumnoCompleto(int id)
        {
            return _context.Alumnos
                .Include(a => a.Plan)
                .Include(a => a.Membresias.Where(m => m.Activa))
                    .ThenInclude(m => m.Plan)
                .Include(a => a.Reservas.Where(r => r.FechaReserva >= DateOnly.FromDateTime(DateTime.Now)))
                    .ThenInclude(r => r.Clase)
                        .ThenInclude(c => c.Profesor)
                .FirstOrDefault(a => a.Id == id);
        }

        public override List<Alumno> GetAll()
        {
            return _context.Alumnos
                .Include(a => a.Plan)
                .ToList();
        }

        public override Alumno? GetById(int id)
        {
            return _context.Alumnos
                .Include(a => a.Plan)
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Alumno> GetAlumnosPorPlan(int planId)
        {
            return _context.Alumnos
                .Where(a => a.PlanId == planId && a.Activo)
                .Include(a => a.Plan)
                .ToList();
        }

        public List<Alumno> GetAlumnosActivos()
        {
            return _context.Alumnos
                .Where(a => a.Activo)
                .Include(a => a.Plan)
                .OrderBy(a => a.Apellido)
                .ThenBy(a => a.Nombre)
                .ToList();
        }

        public bool TieneReservasActivas(int alumnoId)
        {
            return _context.Reservas
                .Any(r => r.AlumnoId == alumnoId &&
                     r.FechaReserva >= DateOnly.FromDateTime(DateTime.Now));
        }
    }
}