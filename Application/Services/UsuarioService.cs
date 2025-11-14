using Application.Abstractions;
using Contract.Requests;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMembresiaRepository _membresiaRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMembresiaRepository membresiaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _membresiaRepository = membresiaRepository;
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

        public bool Desactivar(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null) return false;

            usuario.Activo = false;
            return _usuarioRepository.Update(usuario);
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

        public List<Contract.Responses.UsuarioResponse> GetAllDtos(int? roleId, int? sucursalId)
        {
            // Por ahora delegamos al repositorio sin filtros
            // TODO: Implementar filtros en el repositorio o filtrar aquí
            var usuarios = _usuarioRepository.GetAllDtos();

            // Filtrar por roleId
            if (roleId.HasValue)
            {
                usuarios = usuarios.Where(u => u.RoleId == roleId.Value).ToList();
            }

            // Filtrar por sucursalId
            if (sucursalId.HasValue)
            {
                usuarios = usuarios.Where(u => u.SucursalId == sucursalId.Value).ToList();
            }

            return usuarios;
        }

        public (List<Contract.Responses.UsuarioResponse> Items, int Total) GetPagedDtos(int page, int pageSize, string? q = null)
        {
            return _usuarioRepository.GetPagedDtos(page, pageSize, q);
        }

        public bool Create(RegisterRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return false;
            }

            if (_usuarioRepository.ExistsByEmail(request.Email))
            {
                return false;
            }

            // TODO: Implementar creación de usuario
            // Esto requiere acceso al método Create del repositorio
            return false;
        }

        public bool Update(int id, UpdateUsuarioRequest request)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre))
                usuario.Nombre = request.Nombre;

            if (!string.IsNullOrWhiteSpace(request.Apellido))
                usuario.Apellido = request.Apellido;

            if (!string.IsNullOrWhiteSpace(request.Telefono))
                usuario.Telefono = request.Telefono;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (request.Email != usuario.Email && _usuarioRepository.ExistsByEmail(request.Email))
                    return false;
                usuario.Email = request.Email;
            }

            if (!string.IsNullOrWhiteSpace(request.Genero))
                usuario.Genero = request.Genero;

            if (!string.IsNullOrWhiteSpace(request.Direccion))
                usuario.Direccion = request.Direccion;

            if (!string.IsNullOrWhiteSpace(request.Image))
                usuario.Image = request.Image;

            if (request.FechaNacimiento.HasValue)
                usuario.FechaNacimiento = request.FechaNacimiento.Value;

            if (request.SucursalId.HasValue)
                usuario.SucursalId = request.SucursalId.Value;

            return _usuarioRepository.Update(usuario);
        }
    }
}
