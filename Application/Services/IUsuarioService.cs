using Domain.Entities;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Usuario? GetByEmail(string email);
        Usuario? GetById(int id);
        bool ExistsByEmail(string email);
        // Devuelve la entidad completa (incluye PasswordHash) para autenticación
        Usuario? GetWithPasswordByEmail(string email);
    }
}
