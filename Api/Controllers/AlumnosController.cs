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
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnosController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult CreateAlumno([FromBody] CreateAlumnoRequest request)
        {
            var response = _alumnoService.CreateAlumno(request);
            if (response == null)
                return BadRequest("No se pudo crear el alumno");

            return CreatedAtAction(nameof(GetAlumno), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlumno(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Solo SuperAdmin puede ver cualquier alumno, los alumnos solo pueden ver su propio perfil
            if (userRole != "SuperAdmin" && userRole != "Alumno")
                return Forbid();

            if (userRole == "Alumno" && userId != id)
                return Forbid();

            var response = _alumnoService.GetPerfilCompleto(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Profesor")]
        public IActionResult GetAlumnos()
        {
            var alumnos = _alumnoService.GetAlumnosActivos();
            return Ok(alumnos);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAlumno(int id, [FromBody] CreateAlumnoRequest request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Solo SuperAdmin puede actualizar cualquier alumno, los alumnos solo pueden actualizar su propio perfil
            if (userRole != "SuperAdmin" && (userRole != "Alumno" || userId != id))
                return Forbid();

            var success = _alumnoService.UpdateAlumno(id, request);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteAlumno(int id)
        {
            var success = _alumnoService.DeleteAlumno(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("me")]
        [Authorize(Roles = "Alumno")]
        public IActionResult GetMiPerfil()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var response = _alumnoService.GetPerfilCompleto(userId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}