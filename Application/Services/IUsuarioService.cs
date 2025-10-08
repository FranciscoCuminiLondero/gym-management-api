using Domain.Entities;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Usuario? GetByEmail(string email);
        Usuario? GetById(int id);
        bool ExistsByEmail(string email);
    }
}
