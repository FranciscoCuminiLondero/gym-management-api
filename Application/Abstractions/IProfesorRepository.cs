using Domain.Entities;

namespace Application.Abstractions
{
    public interface IProfesorRepository : IBaseRepository<Profesor>
    {
        Profesor? GetByEmail(string email);
    }
}
