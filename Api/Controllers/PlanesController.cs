using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly IPlanService _planService;
        public PlanesController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<PlanResponse>> GetPlanesActivos()
        {
            var planes = _planService.GetPlanesActivos();
            return Ok(planes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<PlanResponse> GetById(int id)
        {
            var plan = _planService.GetById(id);
            if (plan == null) return NotFound("Plan no encontrado.");
            return Ok(plan);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create(CreatePlanRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _planService.Create(request);
            if (!resultado)
                return BadRequest("No se pudo crear el plan. Verifique los datos.");

            return Ok(new { message = "Plan creado exitosamente." });
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Update(int id, [FromBody] UpdatePlanRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _planService.Update(id, request);
            if (!resultado)
                return NotFound("Plan no encontrado.");

            return Ok(new { message = "Plan actualizado exitosamente." });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int id)
        {
            var resultado = _planService.Delete(id);
            if (!resultado) return NotFound("Plan no encontrado.");

            return Ok(new { message = "Plan eliminado exitosamente." });
        }
    }
}
