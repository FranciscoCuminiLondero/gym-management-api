using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public interface IProfesorService
    {
        List<ProfesorResponse> GetAll();
        ProfesorResponse? GetById(int id);
        bool Create(CreateProfesorRequest request);
    }
}