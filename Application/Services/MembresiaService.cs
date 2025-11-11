using Application.Abstractions;
using Contract.Requests;
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
    }
}
