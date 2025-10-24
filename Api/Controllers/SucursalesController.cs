using Application.Services;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalService _sucursalService;

        public SucursalesController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<SucursalResponse>> GetActivas()
        {
            var sucursales = _sucursalService.GetActivas();
            return Ok(sucursales);
        }

        [HttpGet("all")]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<List<SucursalResponse>> GetAll()
        {
            var sucursales = _sucursalService.GetAll();
            return Ok(sucursales);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<SucursalResponse> GetById(int id)
        {
            var sucursal = _sucursalService.GetById(id);
            if (sucursal == null) return NotFound();
            return Ok(sucursal);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Desactivar(int id)
        {
            var resultado = _sucursalService.Desactivar(id);
            if (!resultado) return NotFound("Sucursal no encontrada.");

            return Ok(new { message = "Sucursal desactivada exitosamente." });
        }
    }
}
