using Application.Services;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalasController : ControllerBase
    {
        private readonly ISalaService _salaService;

        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<SalaResponse>> GetAll()
        {
            var salas = _salaService.GetAll();
            return Ok(salas);
        }

        [HttpGet("sucursal/{sucursalId}")]
        [AllowAnonymous]
        public ActionResult<List<SalaResponse>> GetBySucursalId(int sucursalId)
        {
            var salas = _salaService.GetBySucursalId(sucursalId);
            return Ok(salas);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<SalaResponse> GetById(int id)
        {
            var sala = _salaService.GetById(id);
            if (sala == null) return NotFound();
            return Ok(sala);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Desactivar(int id)
        {
            var resultado = _salaService.Desactivar(id);
            if (!resultado) return NotFound("Sala no encontrada.");

            return Ok(new { message = "Sala desactivada exitosamente." });
        }
    }
}
