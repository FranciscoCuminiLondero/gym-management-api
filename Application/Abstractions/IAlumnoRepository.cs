using Domain.Entities;

namespace Application.Abstractions
{
    public interface IAlumnoRepository : IBaseRepository<Alumno>
    {
        bool ExistsByDni(string dni);
    }
}
