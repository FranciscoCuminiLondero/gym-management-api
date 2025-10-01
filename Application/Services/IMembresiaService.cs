using Contract.Requests;

namespace Application.Services
{
    public interface IMembresiaService
    {
        bool AsociarMembresia(CreateMembresiaRequest request);
    }
}
