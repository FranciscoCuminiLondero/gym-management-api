using Domain.Entities;

namespace Application.Abstractions
{
    public interface IUsuarioRepository
    {
        Usuario? GetByEmail(string email);
        Usuario? GetById(int id);
        bool ExistsByEmail(string email);
    bool ExistsByDni(string dni);
        bool IsActivo(int id);
        // Response helpers
        Contract.Responses.UsuarioResponse? GetDtoByEmail(string email);
        Contract.Responses.UsuarioResponse? GetDtoById(int id);
    List<Contract.Responses.UsuarioResponse> GetAllDtos();
        // Devuelve la entidad completa (incluye PasswordHash) para usos de autenticación.
        Domain.Entities.Usuario? GetWithPasswordByEmail(string email);
        bool HasMembresiaActiva(int alumnoId);
    }
}
