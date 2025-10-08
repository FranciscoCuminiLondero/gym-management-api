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
    }
}