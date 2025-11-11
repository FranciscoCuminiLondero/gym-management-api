using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface ISalaService
    {
        List<SalaResponse> GetAll();
        List<SalaResponse> GetBySucursalId(int sucursalId);
        SalaResponse? GetById(int id);
        bool Update(int id, UpdateSalaRequest request);
        bool Desactivar(int id);
    }
}
