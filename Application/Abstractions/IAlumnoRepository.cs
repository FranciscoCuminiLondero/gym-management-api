using Domain.Entities;

namespace Application.Abstractions
{
    public interface IAlumnoRepository : IBaseRepository<Alumno>
    {
        Alumno? GetByEmail(string email);
        bool ExistsByEmail(string email);
        List<Alumno> GetAlumnosConMembresiaActiva();
        Alumno? GetAlumnoCompleto(int id);
        List<Alumno> GetAlumnosPorPlan(int planId);
        List<Alumno> GetAlumnosActivos();
        bool TieneReservasActivas(int alumnoId);
    }
}