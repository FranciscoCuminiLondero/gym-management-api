using Application.Services;
using Contract.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasesController : ControllerBase
    {
        private readonly IClaseService _claseService;
        public ClasesController(IClaseService claseService)
        {
            _claseService = claseService;
        }

        [HttpGet("fecha/{fecha}")]
        public ActionResult<List<ClaseResponse>> GetPorFecha(DateOnly fecha)
        {
            var clases = _claseService.GetDisponiblesPorFecha(fecha);
            return Ok(clases);
        }

        [HttpGet("{id}")]
        public ActionResult<ClaseResponse> GetById(int id)
        {
            var clase = _claseService.GetById(id);
            if (clase == null) return NotFound();
            return Ok(clase);
        }
    }
}
