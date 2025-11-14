using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembresiasController : ControllerBase
    {
        private readonly IMembresiaService _membresiaService;

        public MembresiasController(IMembresiaService membresiaService)
        {
            _membresiaService = membresiaService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<MembresiaResponse>> GetByAlumno([FromQuery] int? alumnoId)
        {
            if (!alumnoId.HasValue)
                return BadRequest("El parámetro alumnoId es requerido.");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != alumnoId.ToString())
            {
                return StatusCode(403, "No tiene permisos para ver las membresías de otro usuario.");
            }

            var membresias = _membresiaService.GetByAlumnoId(alumnoId.Value);
            return Ok(membresias);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create([FromBody] CreateMembresiaRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _membresiaService.Create(request);
            if (!resultado)
                return BadRequest("No se pudo crear la membresía. Verifique los datos.");

            return Ok(new { message = "Membresía creada exitosamente." });
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Update(int id, [FromBody] UpdateMembresiaRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _membresiaService.Update(id, request);
            if (!resultado)
                return NotFound("Membresía no encontrada.");

            return Ok(new { message = "Membresía actualizada exitosamente." });
        }
    }
}
