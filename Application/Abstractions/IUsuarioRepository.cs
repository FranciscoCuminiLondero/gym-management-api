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
    // Paged + filter by query (searches nombre or email)
    (List<Contract.Responses.UsuarioResponse> Items, int Total) GetPagedDtos(int page, int pageSize, string? q = null);
        // Devuelve la entidad completa (incluye PasswordHash) para usos de autenticación.
        Domain.Entities.Usuario? GetWithPasswordByEmail(string email);
        bool HasMembresiaActiva(int alumnoId);
    }
}
