using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class ClaseRepository : BaseRepository<Clase>, IClaseRepository
    {
        private readonly GymDbContext _context;
        public ClaseRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
        public Clase? GetByIdWithDetails(int id)
        {
            return _context.Clases.FirstOrDefault(c => c.Id == id);
        }
        public List<Clase> GetByProfesorId(int profesorId)
        {
            return _context.Clases
                .Where(c => c.ProfesorId == profesorId && c.Activa)
                .ToList();
        }

        public List<Clase> GetBySucursalId(int sucursalId)
        {
            return _context.Clases
                .Where(c => c.SucursalId == sucursalId && c.Activa)
                .ToList();
        }

        public List<Clase> GetBySalaId(int salaId)
        {
            return _context.Clases
                .Where(c => c.SalaId == salaId && c.Activa)
                .ToList();
        }

        public List<Clase> GetDisponiblesPorFecha(DateOnly fecha)
        {
            return _context.Clases
                .Where(c => c.Fecha == fecha && c.Activa)
                .ToList();
        }

        public bool TieneConflictoHorario(int profesorId, DateOnly fecha, TimeOnly horaInicio, int duracionMinutos)
        {
            var horaFin = horaInicio.AddMinutes(duracionMinutos);

            return _context.Clases.Any(c =>
                c.ProfesorId == profesorId &&
                c.Fecha == fecha &&
                c.Activa &&
                (
                    (horaInicio >= c.HoraInicio && horaInicio < c.HoraInicio.AddMinutes(c.DuracionMinutos)) ||
                    (horaFin > c.HoraInicio && horaFin <= c.HoraInicio.AddMinutes(c.DuracionMinutos)) ||
                    (horaInicio <= c.HoraInicio && horaFin >= c.HoraInicio.AddMinutes(c.DuracionMinutos))
                )
            );
        }
    }
}
