using Application.Abstractions;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;

namespace Application.Services
{
    public class MembresiaService : IMembresiaService
    {
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IPagoService _pagoService;

        public MembresiaService(
            IMembresiaRepository membresiaRepository,
            IAlumnoRepository alumnoRepository,
            IUsuarioRepository usuarioRepository,
            IPlanRepository planRepository,
            IPagoService pagoService)
        {
            _membresiaRepository = membresiaRepository;
            _alumnoRepository = alumnoRepository;
            _usuarioRepository = usuarioRepository;
            _planRepository = planRepository;
            _pagoService = pagoService;
        }

        public bool AsociarMembresia(CreateMembresiaRequest request)
        {
            if (!_usuarioRepository.IsActivo(request.AlumnoId))
                return false;

            var plan = _planRepository.GetById(request.PlanId);
            if (plan == null || !plan.Activo)
                return false;

            if (_membresiaRepository.TieneMembresiaActiva(request.AlumnoId))
                return false;

            var membresia = new Membresia
            {
                AlumnoId = request.AlumnoId,
                PlanId = request.PlanId,
                FechaInicio = DateOnly.FromDateTime(DateTime.Today),
                FechaFin = DateOnly.FromDateTime(DateTime.Today).AddDays(plan.DuracionDias),
                Activa = true
            };

            if (!_membresiaRepository.Create(membresia))
                return false;

            _pagoService.RegistrarPagoInicial(membresia.Id, plan.Precio);

            return true;
        }

        public bool Create(CreateMembresiaRequest request)
        {
            return AsociarMembresia(request);
        }

        public bool Update(int id, UpdateMembresiaRequest request)
        {
            var membresia = _membresiaRepository.GetById(id);
            if (membresia == null) return false;

            if (request.PlanId.HasValue)
                membresia.PlanId = request.PlanId.Value;

            if (request.FechaInicio.HasValue)
                membresia.FechaInicio = request.FechaInicio.Value;

            if (request.FechaFin.HasValue)
                membresia.FechaFin = request.FechaFin.Value;

            if (request.Activa.HasValue)
                membresia.Activa = request.Activa.Value;

            return _membresiaRepository.Update(membresia);
        }

        public List<MembresiaResponse> GetByAlumnoId(int alumnoId)
        {
            var membresias = _membresiaRepository.GetByCriterial(m => m.AlumnoId == alumnoId);

            return membresias.Select(m => new MembresiaResponse
            {
                Id = m.Id,
                PlanId = m.PlanId,
                AlumnoId = m.AlumnoId,
                FechaInicio = m.FechaInicio.ToString("yyyy-MM-dd"),
                FechaFin = m.FechaFin.ToString("yyyy-MM-dd"),
                Estado = m.Activa && m.FechaFin >= DateOnly.FromDateTime(DateTime.Today) ? "activa" : "expirada",
                Activa = m.Activa
            }).ToList();
        }
    }
}
