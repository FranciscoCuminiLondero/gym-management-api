using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public List<SalaResponse> GetAll()
        {
            var salas = _salaRepository.GetAll();
            return salas.Select(s => new SalaResponse
            {
                Id = s.Id,
                SucursalId = s.SucursalId,
                Nombre = s.Nombre,
                Tipo = s.Tipo,
                Capacidad = s.Capacidad,
                Descripcion = s.Descripcion,
                Activa = s.Activa
            }).ToList();
        }

        public List<SalaResponse> GetBySucursalId(int sucursalId)
        {
            var salas = _salaRepository.GetBySucursalId(sucursalId);
            return salas.Select(s => new SalaResponse
            {
                Id = s.Id,
                SucursalId = s.SucursalId,
                Nombre = s.Nombre,
                Tipo = s.Tipo,
                Capacidad = s.Capacidad,
                Descripcion = s.Descripcion,
                Activa = s.Activa
            }).ToList();
        }

        public SalaResponse? GetById(int id)
        {
            var sala = _salaRepository.GetById(id);
            if (sala == null) return null;

            return new SalaResponse
            {
                Id = sala.Id,
                SucursalId = sala.SucursalId,
                Nombre = sala.Nombre,
                Tipo = sala.Tipo,
                Capacidad = sala.Capacidad,
                Descripcion = sala.Descripcion,
                Activa = sala.Activa
            };
        }

        public bool Create(CreateSalaRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Nombre) ||
                request.Capacidad <= 0)
            {
                return false;
            }

            var nuevaSala = new Sala
            {
                SucursalId = request.SucursalId,
                Nombre = request.Nombre,
                Tipo = request.Tipo,
                Capacidad = request.Capacidad,
                Descripcion = request.Descripcion,
                Activa = true
            };

            return _salaRepository.Create(nuevaSala);
        }

        public bool Update(int id, UpdateSalaRequest request)
        {
            var sala = _salaRepository.GetById(id);
            if (sala == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Nombre))
                sala.Nombre = request.Nombre;

            if (!string.IsNullOrWhiteSpace(request.Tipo))
                sala.Tipo = request.Tipo;

            if (request.Capacidad.HasValue)
                sala.Capacidad = request.Capacidad.Value;

            if (!string.IsNullOrWhiteSpace(request.Descripcion))
                sala.Descripcion = request.Descripcion;

            return _salaRepository.Update(sala);
        }

        public bool Desactivar(int id)
        {
            var sala = _salaRepository.GetById(id);
            if (sala == null) return false;

            sala.Activa = false;
            return _salaRepository.Update(sala);
        }
    }
}
