using Domain.Entities;

namespace Application.Abstractions
{
    public interface IProfesorRepository : IBaseRepository<Profesor>
    {
        bool ExistsByEmail(string email);
        Profesor? GetByEmail(string email);
        bool IsActivo(int profesorId);
    }
}
