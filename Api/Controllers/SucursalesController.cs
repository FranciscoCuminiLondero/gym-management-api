using Application.Services;
using Contract.Requests;
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

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create([FromBody] CreateSucursalRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _sucursalService.Create(request);
            if (!resultado)
                return BadRequest("No se pudo crear la sucursal. Verifique los datos.");

            return Ok(new { message = "Sucursal creada exitosamente." });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Update(int id, [FromBody] UpdateSucursalRequest request)
        {
            var resultado = _sucursalService.Update(id, request);
            if (!resultado) return NotFound("Sucursal no encontrada.");

            return Ok(new { message = "Sucursal actualizada exitosamente." });
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
