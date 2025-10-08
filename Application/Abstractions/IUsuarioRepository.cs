using Domain.Entities;

namespace Application.Abstractions
{
    public interface IUsuarioRepository
    {
        Usuario? GetByEmail(string email);
        Usuario? GetById(int id);
        bool ExistsByEmail(string email);
        // Response helpers
        Contract.Responses.UsuarioResponse? GetDtoByEmail(string email);
        Contract.Responses.UsuarioResponse? GetDtoById(int id);
        // Devuelve la entidad completa (incluye PasswordHash) para usos de autenticación.
        Domain.Entities.Usuario? GetWithPasswordByEmail(string email);
    }
}
