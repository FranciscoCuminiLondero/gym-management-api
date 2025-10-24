using Domain.Entities;

namespace Application.Abstractions
{
    public interface IReservaRepository : IBaseRepository<Reserva>
    {
        bool ExistsByAlumnoAndClase(int alumnoId, int claseId);
        bool ExistsByAlumnoAndFecha(int alumnoId, DateOnly fecha);
        List<Reserva> GetByAlumnoId(int alumnoId);
        List<Reserva> GetByClaseId(int claseId);
    }
}
