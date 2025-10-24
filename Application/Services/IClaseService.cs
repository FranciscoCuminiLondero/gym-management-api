using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IClaseService
    {
        List<ClaseResponse> GetAll();
        List<ClaseResponse> GetDisponiblesPorFecha(DateOnly fecha);
        ClaseResponse? GetById(int id);
        bool Create(CreateClaseRequest request);
        bool Delete(int id);
    }
}
