using Application.Services;
using Contract.Responses;
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
        public ActionResult<List<UsuarioResponse>> GetAll()
        {
            var usuarios = _usuarioService
                .GetAllDtos();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioResponse> GetById(int id)
        {
            var usuario = _usuarioService.GetDtoById(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpGet("by-email")]
        public ActionResult<UsuarioResponse> GetByEmail([FromQuery] string email)
        {
            var usuario = _usuarioService.GetDtoByEmail(email);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
    }
}
