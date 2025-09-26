using Domain.Entities;

namespace Application.Abstractions
{
    public interface IClaseRepository : IBaseRepository<Clase>
    {
        Clase? GetByIdWithDetails(int id);
        List<Clase> GetByProfesorId(int profesorId);
        List<Clase> GetBySucursalId(int sucursalId);
        List<Clase> GetBySalaId(int salaId);
        List<Clase> GetDisponiblesPorFecha(DateOnly fecha);
    }
}
