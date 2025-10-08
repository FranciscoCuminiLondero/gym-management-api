using Domain.Entities;

namespace Application.Abstractions
{
    public interface IProfesorRepository : IBaseRepository<Profesor>
    {
        Profesor? GetByEmail(string email);
        bool ExistsByEmail(string email);
        List<Profesor> GetProfesoresConClases();
        Profesor? GetProfesorCompleto(int id);
        List<Profesor> GetProfesoresPorEspecialidad(string especialidad);
        List<Profesor> GetProfesoresActivos();
        List<Profesor> GetProfesoresDisponibles(DateTime fecha, TimeOnly horaInicio, TimeOnly horaFin);
        bool TieneClasesAsignadas(int profesorId);
        int GetCantidadClasesProfesor(int profesorId, DateOnly fechaDesde, DateOnly fechaHasta);
    }
}