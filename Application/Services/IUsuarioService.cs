using Domain.Entities;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Usuario? GetByEmail(string email);
        Usuario? GetById(int id);
        bool ExistsByEmail(string email);
        bool ExistsByDni(string dni);
        // Devuelve la entidad completa (incluye PasswordHash) para autenticación
        Usuario? GetWithPasswordByEmail(string email);
        // DTO helpers
        Contract.Responses.UsuarioResponse? GetDtoByEmail(string email);
        Contract.Responses.UsuarioResponse? GetDtoById(int id);
        List<Contract.Responses.UsuarioResponse> GetAllDtos();
        (List<Contract.Responses.UsuarioResponse> Items, int Total) GetPagedDtos(int page, int pageSize, string? q = null);
    }
}
