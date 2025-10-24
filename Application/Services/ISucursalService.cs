using Contract.Responses;

namespace Application.Services
{
    public interface ISucursalService
    {
        List<SucursalResponse> GetAll();
        List<SucursalResponse> GetActivas();
        SucursalResponse? GetById(int id);
        bool Desactivar(int id);
    }
}
