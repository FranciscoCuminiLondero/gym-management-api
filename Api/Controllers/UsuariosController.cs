using Application.Services;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<List<UsuarioResponse>> GetAll()
        {
            var items = _usuarioService.GetAllDtos();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<UsuarioResponse> GetById(int id)
        {
            var usuario = _usuarioService.GetDtoById(id);
            if (usuario == null) return NotFound();

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para ver este usuario.");
            }

            return Ok(usuario);
        }

        [HttpGet("by-email")]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<UsuarioResponse> GetByEmail([FromQuery] string email)
        {
            var usuario = _usuarioService.GetDtoByEmail(email);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Desactivar(int id)
        {
            var resultado = _usuarioService.Desactivar(id);
            if (!resultado) return NotFound("Usuario no encontrado.");

            return Ok(new { message = "Usuario desactivado exitosamente." });
        }
    }
}
