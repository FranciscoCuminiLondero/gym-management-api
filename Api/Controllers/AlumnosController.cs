using Application.Abstractions;
using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;
        private readonly IMembresiaRepository _membresiaRepository;
        private readonly IReservaRepository _reservaRepository;
        public AlumnosController(
            IAlumnoService alumnoService, 
            IMembresiaRepository membresiaRepository,
            IReservaRepository reservaRepository)
        {
            _alumnoService = alumnoService;
            _membresiaRepository = membresiaRepository;
            _reservaRepository = reservaRepository;
        }

        [HttpGet]
        public ActionResult<List<AlumnoResponse>> GetAll()
        {
            var alumnos = _alumnoService.GetAll();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public ActionResult<AlumnoResponse> GetById(int id)
        {
            var alumno = _alumnoService.GetById(id);
            if (alumno == null) return NotFound();
            return Ok(alumno);
        }

        [HttpGet("perfil")]
        public ActionResult<AlumnoPerfilResponse> GetPerfil(int alumnoId) // id luego viene del token
        {
            var alumno = _alumnoService.GetById(alumnoId);
            if(alumno == null) return NotFound();

            var membresiaActiva = _membresiaRepository.GetByCriterial(m=> m.AlumnoId == alumnoId && m.Activa).FirstOrDefault();

            var reservas = _reservaRepository.GetByAlumnoId(alumnoId);

            var perfil = new AlumnoPerfilResponse
            {
                Alumno = alumno,
                MembresiaActiva = membresiaActiva == null ? null : new MembresiaResponse
                {
                    Id = membresiaActiva.Id,
                    PlanId = membresiaActiva.PlanId,
                    FechaInicio = membresiaActiva.FechaInicio,
                    FechaFin = membresiaActiva.FechaFin,
                    Activa = membresiaActiva.Activa
                },
                Reservas = reservas.Select(r => new ReservaResponse
                {
                    Id = r.Id,
                    ClaseId = r.ClaseId,
                    FechaReserva = r.FechaReserva,
                    Activo = r.Activo
                }).ToList()
            };

            return Ok(perfil);

        }
    }
}
