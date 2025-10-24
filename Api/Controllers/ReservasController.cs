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

        [HttpPost]
        [Authorize]
        public ActionResult<bool> Create(CreateReservaRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud no puede ser nula.");
            }

            var resultado = _reservaService.Create(request);
            if (!resultado)
            {
                return BadRequest("No se pudo crear la reserva. Verifique que el alumno y la clase existan, y que no haya una reserva duplicada.");
            }

            return Ok(true);
        }

        [HttpGet("alumno/{alumnoId}")]
        [Authorize]
        public ActionResult<List<ReservaResponse>> GetByAlumnoId(int alumnoId)
        {
            var reservas = _reservaService.GetByAlumnoId(alumnoId);
            return Ok(reservas);
        }

        [HttpGet("clase/{claseId}")]
        [Authorize]
        public ActionResult<List<ReservaResponse>> GetByClaseId(int claseId)
        {
            var reservas = _reservaService.GetByClaseId(claseId);
            return Ok(reservas);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var resultado = _reservaService.Delete(id);
            if (!resultado) return NotFound("Reserva no encontrada.");

            return Ok(new { message = "Reserva cancelada exitosamente." });
        }
    }
}
