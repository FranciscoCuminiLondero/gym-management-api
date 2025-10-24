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
        bool Update(Usuario usuario);
        Contract.Responses.UsuarioResponse? GetDtoByEmail(string email);
        Contract.Responses.UsuarioResponse? GetDtoById(int id);
        List<Contract.Responses.UsuarioResponse> GetAllDtos();
        (List<Contract.Responses.UsuarioResponse> Items, int Total) GetPagedDtos(int page, int pageSize, string? q = null);
        Domain.Entities.Usuario? GetWithPasswordByEmail(string email);
        bool HasMembresiaActiva(int alumnoId);
    }
}
