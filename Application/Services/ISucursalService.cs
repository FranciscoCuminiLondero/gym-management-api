using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface ISucursalService
    {
        List<SucursalResponse> GetAll();
        List<SucursalResponse> GetActivas();
        SucursalResponse? GetById(int id);
        bool Create(CreateSucursalRequest request);
        bool Update(int id, UpdateSucursalRequest request);
        bool Desactivar(int id);
    }
}
