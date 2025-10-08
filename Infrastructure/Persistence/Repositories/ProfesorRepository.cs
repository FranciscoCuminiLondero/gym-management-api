using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            => _context.Profesores.FirstOrDefault(p => p.Email == email);

        public bool ExistsByEmail(string email)
            => _context.Profesores.Any(p => p.Email == email);

        public List<Profesor> GetProfesoresConClases()
        {
            return _context.Profesores
                .Include(p => p.Clases.Where(c => c.Activa))
                    .ThenInclude(c => c.Sala)
                .Include(p => p.Clases.Where(c => c.Activa))
                    .ThenInclude(c => c.Sucursal)
                .Where(p => p.Activo)
                .ToList();
        }

        public Profesor? GetProfesorCompleto(int id)
        {
            return _context.Profesores
                .Include(p => p.Clases.Where(c => c.Activa))
                    .ThenInclude(c => c.Sala)
                .Include(p => p.Clases.Where(c => c.Activa))
                    .ThenInclude(c => c.Sucursal)
                .Include(p => p.Clases.Where(c => c.Activa))
                    .ThenInclude(c => c.Reservas.Where(r => r.FechaReserva >= DateOnly.FromDateTime(DateTime.Now)))
                        .ThenInclude(r => r.Alumno)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Profesor> GetProfesoresPorEspecialidad(string especialidad)
        {
            return _context.Profesores
                .Where(p => p.Especialidad != null &&
                           p.Especialidad.ToLower().Contains(especialidad.ToLower()) &&
                           p.Activo)
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        public override List<Profesor> GetAll()
        {
            return _context.Profesores
                .Include(p => p.Clases.Where(c => c.Activa))
                .ToList();
        }

        public override Profesor? GetById(int id)
        {
            return _context.Profesores
                .Include(p => p.Clases.Where(c => c.Activa))
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Profesor> GetProfesoresActivos()
        {
            return _context.Profesores
                .Where(p => p.Activo)
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        public List<Profesor> GetProfesoresDisponibles(DateTime fecha, TimeOnly horaInicio, TimeOnly horaFin)
        {
            var fechaSolo = DateOnly.FromDateTime(fecha);

            return _context.Profesores
                .Where(p => p.Activo &&
                       !p.Clases.Any(c => c.Fecha == fechaSolo &&
                                         c.Activa &&
                                         ((c.HoraInicio >= horaInicio && c.HoraInicio < horaFin) ||
                                          (c.HoraInicio < horaInicio &&
                                           c.HoraInicio.AddMinutes(c.DuracionMinutos) > horaInicio))))
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        public bool TieneClasesAsignadas(int profesorId)
        {
            return _context.Clases
                .Any(c => c.ProfesorId == profesorId &&
                     c.Activa &&
                     c.Fecha >= DateOnly.FromDateTime(DateTime.Now));
        }

        public int GetCantidadClasesProfesor(int profesorId, DateOnly fechaDesde, DateOnly fechaHasta)
        {
            return _context.Clases
                .Count(c => c.ProfesorId == profesorId &&
                           c.Activa &&
                           c.Fecha >= fechaDesde &&
                           c.Fecha <= fechaHasta);
        }
    }
}