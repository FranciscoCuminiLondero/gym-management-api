using Domain.Entities;

namespace Application.Abstractions
{
    public interface IAlumnoRepository : IBaseRepository<Alumno>
    {
        Alumno? GetByIdWithMembresias(int id);
        bool IsActivo(int id);
    }
}
