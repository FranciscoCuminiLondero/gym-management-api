using Application.Services;
using Contract.Responses;
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
        public ActionResult<List<PlanResponse>> GetPlanesActivos()
        {
            var planes = _planService.GetPlanesActivos();
            return Ok(planes);
        }
    }
}
