using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IProfesorService
    {
        ProfesorResponse? CreateProfesor(CreateProfesorRequest request);
        ProfesorResponse? GetProfesorById(int id);
        List<ProfesorResponse> GetProfesoresActivos();
        bool UpdateProfesor(int id, CreateProfesorRequest request);
        bool DeleteProfesor(int id);
        ProfesorResponse? GetPerfilCompleto(int id);
        List<ProfesorResponse> GetProfesoresPorEspecialidad(string especialidad);
    }
}