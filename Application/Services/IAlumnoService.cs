using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IAlumnoService
    {
        AlumnoResponse? CreateAlumno(CreateAlumnoRequest request);
        AlumnoResponse? GetAlumnoById(int id);
        List<AlumnoResponse> GetAlumnosActivos();
        bool UpdateAlumno(int id, CreateAlumnoRequest request);
        bool DeleteAlumno(int id);
        AlumnoResponse? GetPerfilCompleto(int id);
    }
}