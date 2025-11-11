using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IReservaService
    {
        bool Create(CreateReservaRequest request);
        List<ReservaResponse> GetByAlumnoId(int alumnoId);
        List<ReservaResponse> GetByClaseId(int claseId);
        int? GetAlumnoIdByReservaId(int reservaId);
        bool Delete(int id);
    }
}
