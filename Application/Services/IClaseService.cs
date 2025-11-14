using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IClaseService
    {
        List<ClaseResponse> GetAll();
        List<ClaseResponse> GetDisponiblesPorFecha(DateOnly fecha);
        List<ClaseResponse> GetByProfesorId(int profesorId);
        List<ClaseResponse> GetBySucursalId(int sucursalId);
        ClaseResponse? GetById(int id);
        int? GetProfesorIdByClaseId(int claseId);
        bool Create(CreateClaseRequest request);
        bool Update(int id, UpdateClaseRequest request);
        bool Delete(int id);
    }
}
