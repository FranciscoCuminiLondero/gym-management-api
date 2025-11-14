using Application.Services;
using Contract.Requests;
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
        public ActionResult<List<UsuarioResponse>> GetAll([FromQuery] int? roleId, [FromQuery] int? sucursalId)
        {
            var items = _usuarioService.GetAllDtos(roleId, sucursalId);
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

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create([FromBody] RegisterRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email y contraseña son obligatorios.");

            var resultado = _usuarioService.Create(request);
            if (!resultado)
                return BadRequest("No se pudo crear el usuario. Verifique los datos.");

            return Ok(new { message = "Usuario creado exitosamente." });
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] UpdateUsuarioRequest request)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para modificar este usuario.");
            }

            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _usuarioService.Update(id, request);
            if (!resultado)
                return BadRequest("No se pudo actualizar el usuario. Verifique los datos.");

            return Ok(new { message = "Usuario actualizado exitosamente." });
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
