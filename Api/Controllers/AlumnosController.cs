using Application.Abstractions;
using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<List<AlumnoResponse>> GetAll()
        {
            var alumnos = _alumnoService.GetAll();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<AlumnoResponse> GetById(int id)
        {
            var alumno = _alumnoService.GetById(id);
            if (alumno == null) return NotFound();

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para ver este alumno.");
            }

            return Ok(alumno);
        }

        [HttpGet("perfil")]
        [Authorize]
        public ActionResult<AlumnoPerfilResponse> GetPerfil(int alumnoId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != alumnoId.ToString())
            {
                return StatusCode(403, "No tiene permisos para ver este perfil.");
            }

            var alumno = _alumnoService.GetById(alumnoId);
            if (alumno == null) return NotFound();

            var membresiaActiva = _membresiaRepository.GetByCriterial(m => m.AlumnoId == alumnoId && m.Activa).FirstOrDefault();

            var reservas = _reservaRepository.GetByAlumnoId(alumnoId);

            var perfil = new AlumnoPerfilResponse
            {
                Alumno = alumno,
                MembresiaActiva = membresiaActiva == null ? null : new MembresiaResponse
                {
                    Id = membresiaActiva.Id,
                    PlanId = membresiaActiva.PlanId,
                    FechaInicio = membresiaActiva.FechaInicio.ToString("yyyy-MM-dd"),
                    FechaFin = membresiaActiva.FechaFin.ToString("yyyy-MM-dd"),
                    Activa = membresiaActiva.Activa
                },
                Reservas = reservas.Select(r => new ReservaResponse
                {
                    Id = r.Id,
                    ClaseId = r.ClaseId,
                    FechaReserva = r.FechaReserva.ToString("yyyy-MM-dd"),
                    Activo = r.Activo
                }).ToList()
            };

            return Ok(perfil);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, UpdateAlumnoRequest request)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para modificar este alumno.");
            }

            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _alumnoService.Update(id, request);
            if (!resultado)
                return BadRequest("No se pudo actualizar el alumno. Verifique los datos.");

            return Ok(new { message = "Alumno actualizado exitosamente." });
        }
    }
}