using Domain.Entities;

namespace Application.Abstractions
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario? GetByEmail(string email);
        bool ExistsByEmail(string email);
        T? GetById<T>(int id) where T : Usuario;
    }
}
