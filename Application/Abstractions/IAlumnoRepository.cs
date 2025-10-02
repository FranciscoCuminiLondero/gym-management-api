using Domain.Entities;

namespace Application.Abstractions
{
    public interface IAlumnoRepository : IBaseRepository<Alumno>
    {
        bool ExistsByDni(string dni);
        bool ExistsByEmail(string email);
        Alumno? GetByIdWithMembresias(int id);
        bool IsActivo(int id);
        bool HasMembresiaActiva(int alumnoId);
    }
}
