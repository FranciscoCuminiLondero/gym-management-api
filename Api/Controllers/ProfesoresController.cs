using Application.Services;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpGet]
        public ActionResult<List<ProfesorResponse>> GetAll()
        {
            var profesores = _profesorService.GetAll();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public ActionResult<ProfesorResponse> GetById(int id)
        {
            var profesor = _profesorService.GetById(id);
            if (profesor == null)
            {
                return NotFound("Profesor no encontrado.");
            }
            return Ok(profesor);
        }
    }
}
