using Domain.Entities;

namespace Application.Abstractions
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        bool ExistsByAlumnoAndClase(int alumnoId, int claseId);
        List<Reserva> GetByAlumnoId(int alumnoId);
        List<Reserva> GetByClaseId(int claseId);
    }
}
