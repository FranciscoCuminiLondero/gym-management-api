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
    public ActionResult<Contract.Responses.PagedResponse<UsuarioResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? q = null)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 20;

            var (items, total) = _usuarioService.GetPagedDtos(page, pageSize, q);

            var result = new Contract.Responses.PagedResponse<UsuarioResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = items
            };

            return Ok(result);
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
