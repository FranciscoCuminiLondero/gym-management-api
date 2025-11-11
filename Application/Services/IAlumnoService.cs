using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IAlumnoService
    {
        List<AlumnoResponse> GetAll();
        AlumnoResponse? GetById(int id);
        bool Create(CreateAlumnoRequest request);
        bool Update(int id, UpdateAlumnoRequest request);
    }
}
