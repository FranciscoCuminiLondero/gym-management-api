using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpGet]
        public ActionResult<List<ProfesorResponse>> GetAll()
        {
            var profesores = _profesorService.GetAll();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public ActionResult<ProfesorResponse> GetById(int id)
        {
            var profesor = _profesorService.GetById(id);
            if (profesor == null)
            {
                return NotFound("Profesor no encontrado.");
            }
            return Ok(profesor);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, UpdateProfesorRequest request)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para modificar este profesor.");
            }

            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _profesorService.Update(id, request);
            if (!resultado)
                return BadRequest("No se pudo actualizar el profesor. Verifique los datos.");

            return Ok(new { message = "Profesor actualizado exitosamente." });
        }
    }
}
