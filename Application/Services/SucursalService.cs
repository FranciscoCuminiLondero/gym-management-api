using Application.Abstractions;
using Contract.Responses;

namespace Application.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository _sucursalRepository;

        public SucursalService(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public List<SucursalResponse> GetAll()
        {
            var sucursales = _sucursalRepository.GetAll();
            return sucursales.Select(s => new SucursalResponse
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Direccion = s.Direccion,
                Telefono = s.Telefono,
                Email = s.Email,
                Activa = s.Activa
            }).ToList();
        }

        public List<SucursalResponse> GetActivas()
        {
            var sucursales = _sucursalRepository.GetActivas();
            return sucursales.Select(s => new SucursalResponse
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Direccion = s.Direccion,
                Telefono = s.Telefono,
                Email = s.Email,
                Activa = s.Activa
            }).ToList();
        }

        public SucursalResponse? GetById(int id)
        {
            var sucursal = _sucursalRepository.GetById(id);
            if (sucursal == null) return null;

            return new SucursalResponse
            {
                Id = sucursal.Id,
                Nombre = sucursal.Nombre,
                Direccion = sucursal.Direccion,
                Telefono = sucursal.Telefono,
                Email = sucursal.Email,
                Activa = sucursal.Activa
            };
        }

        public bool Desactivar(int id)
        {
            var sucursal = _sucursalRepository.GetById(id);
            if (sucursal == null) return false;

            sucursal.Activa = false;
            return _sucursalRepository.Update(sucursal);
        }
    }
}
