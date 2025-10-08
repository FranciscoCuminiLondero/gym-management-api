using Application.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? GetByEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

        public Usuario? GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public bool ExistsByEmail(string email)
        {
            return _usuarioRepository.ExistsByEmail(email);
        }

        public bool ExistsByDni(string dni)
        {
            return _usuarioRepository.ExistsByDni(dni);
        }

        public Usuario? GetWithPasswordByEmail(string email)
        {
            return _usuarioRepository.GetWithPasswordByEmail(email);
        }

        public Contract.Responses.UsuarioResponse? GetDtoByEmail(string email)
        {
            return _usuarioRepository.GetDtoByEmail(email);
        }

        public Contract.Responses.UsuarioResponse? GetDtoById(int id)
        {
            return _usuarioRepository.GetDtoById(id);
        }

        public List<Contract.Responses.UsuarioResponse> GetAllDtos()
        {
            return _usuarioRepository.GetAllDtos();
        }

        public (List<Contract.Responses.UsuarioResponse> Items, int Total) GetPagedDtos(int page, int pageSize, string? q = null)
        {
            return _usuarioRepository.GetPagedDtos(page, pageSize, q);
        }
    }
}
