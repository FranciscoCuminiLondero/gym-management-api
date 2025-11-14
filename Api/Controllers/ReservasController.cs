using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ReservaResponse>> Get([FromQuery] int? alumnoId, [FromQuery] int? claseId)
        {
            if (alumnoId.HasValue)
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

                if (!isAdmin && userIdClaim != alumnoId.ToString())
                {
                    return StatusCode(403, "No tiene permisos para ver las reservas de otro usuario.");
                }

                var reservas = _reservaService.GetByAlumnoId(alumnoId.Value);
                return Ok(reservas);
            }

            if (claseId.HasValue)
            {
                var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");
                var reservas = _reservaService.GetByClaseId(claseId.Value);

                if (isAdmin)
                {
                    return Ok(new
                    {
                        total = reservas.Count,
                        reservas = reservas
                    });
                }

                return Ok(new
                {
                    total = reservas.Count
                });
            }

            return BadRequest("Debe especificar alumnoId o claseId");
        }

        [HttpPost]
        [Authorize]
        public ActionResult<bool> Create(CreateReservaRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud no puede ser nula.");
            }

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != request.AlumnoId.ToString())
            {
                return StatusCode(403, "No tiene permisos para crear reservas para otro usuario.");
            }

            var resultado = _reservaService.Create(request);
            if (!resultado)
            {
                return BadRequest("No se pudo crear la reserva. Verifique que el alumno y la clase existan, y que no haya una reserva duplicada.");
            }

            return Ok(true);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] dynamic request)
        {
            // Para compatibilidad con frontend, acepta PATCH aunque no se implemente aún
            return StatusCode(501, "Actualización de reservas no implementada aún");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            var alumnoIdReserva = _reservaService.GetAlumnoIdByReservaId(id);
            if (alumnoIdReserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            if (!isAdmin && userIdClaim != alumnoIdReserva.ToString())
            {
                return StatusCode(403, "No tiene permisos para eliminar esta reserva.");
            }

            var resultado = _reservaService.Delete(id);
            if (!resultado) return NotFound("Reserva no encontrada.");

            return Ok(new { message = "Reserva cancelada exitosamente." });
        }
    }
}
