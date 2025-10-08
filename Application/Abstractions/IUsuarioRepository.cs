using Domain.Entities;

namespace Application.Abstractions
{
    public interface IUsuarioRepository
    {
        Usuario? GetByEmail(string email);
        Usuario? GetById(int id);
        bool ExistsByEmail(string email);
    }
}
