using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;
        private readonly IProfesorService _profesorService;

        public UsuariosController(IAlumnoService alumnoService, IProfesorService profesorService)
        {
            _alumnoService = alumnoService;
            _profesorService = profesorService;
        }

        [HttpGet("me")]
        public IActionResult GetPerfil()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return role switch
            {
                "Alumno" => GetAlumnoPerfil(userId),
                "Profesor" => GetProfesorPerfil(userId),
                _ => Unauthorized()
            };
        }

        private IActionResult GetAlumnoPerfil(int userId)
        {
            var alumno = _alumnoService.GetPerfilCompleto(userId);
            return alumno != null ? Ok(alumno) : NotFound();
        }

        private IActionResult GetProfesorPerfil(int userId)
        {
            var profesor = _profesorService.GetPerfilCompleto(userId);
            return profesor != null ? Ok(profesor) : NotFound();
        }
    }
}
