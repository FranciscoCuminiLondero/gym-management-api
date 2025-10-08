using Application.Services;
using Contract.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult CreateProfesor([FromBody] CreateProfesorRequest request)
        {
            var response = _profesorService.CreateProfesor(request);
            if (response == null)
                return BadRequest("No se pudo crear el profesor");

            return CreatedAtAction(nameof(GetProfesor), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfesor(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Solo SuperAdmin puede ver cualquier profesor, los profesores solo pueden ver su propio perfil
            if (userRole != "SuperAdmin" && userRole != "Profesor")
                return Forbid();

            if (userRole == "Profesor" && userId != id)
                return Forbid();

            var response = _profesorService.GetPerfilCompleto(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetProfesores()
        {
            var profesores = _profesorService.GetProfesoresActivos();
            return Ok(profesores);
        }

        [HttpGet("especialidad/{especialidad}")]
        public IActionResult GetProfesoresPorEspecialidad(string especialidad)
        {
            var profesores = _profesorService.GetProfesoresPorEspecialidad(especialidad);
            return Ok(profesores);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfesor(int id, [FromBody] CreateProfesorRequest request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Solo SuperAdmin puede actualizar cualquier profesor, los profesores solo pueden actualizar su propio perfil
            if (userRole != "SuperAdmin" && (userRole != "Profesor" || userId != id))
                return Forbid();

            var success = _profesorService.UpdateProfesor(id, request);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteProfesor(int id)
        {
            var success = _profesorService.DeleteProfesor(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("me")]
        [Authorize(Roles = "Profesor")]
        public IActionResult GetMiPerfil()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var response = _profesorService.GetPerfilCompleto(userId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}