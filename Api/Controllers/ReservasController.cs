using Application.Services;
using Contract.Requests;
using Contract.Responses;
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
        public ActionResult<List<ReservaResponse>> GetByAlumnoId(int alumnoId)
        {
            var reservas = _reservaService.GetByAlumnoId(alumnoId);
            return Ok(reservas);
        }

        [HttpGet("clase/{claseId}")]
        public ActionResult<List<ReservaResponse>> GetByClaseId(int claseId)
        {
            var reservas = _reservaService.GetByClaseId(claseId);
            return Ok(reservas);
        }
    }
}
