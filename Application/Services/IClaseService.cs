using Contract.Responses;

namespace Application.Services
{
    public interface IClaseService
    {
        List<ClaseResponse> GetDisponiblesPorFecha(DateOnly fecha);
        ClaseResponse? GetById(int id);
    }
}
