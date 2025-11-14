using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;
        private readonly IClaseService _claseService;

        public ProfesoresController(IProfesorService profesorService, IClaseService claseService)
        {
            _profesorService = profesorService;
            _claseService = claseService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");
            var profesores = _profesorService.GetAll();

            if (isAdmin)
            {
                return Ok(profesores);
            }

            var profesoresPublicos = profesores.Select(p => new ProfesorPublicResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Activo = p.Activo
            }).ToList();

            return Ok(profesoresPublicos);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var profesor = _profesorService.GetById(id);
            if (profesor == null)
            {
                return NotFound("Profesor no encontrado.");
            }

            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (isAdmin)
            {
                return Ok(profesor);
            }

            var profesorPublico = new ProfesorPublicResponse
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                Activo = profesor.Activo
            };

            return Ok(profesorPublico);
        }

        [HttpGet("{id}/clases")]
        [Authorize]
        public ActionResult<List<ClaseResponse>> GetClasesByProfesorId(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para ver las clases de otro profesor.");
            }

            var clases = _claseService.GetByProfesorId(id);
            return Ok(clases);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Create([FromBody] CreateProfesorRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede estar vacía.");

            var resultado = _profesorService.Create(request);
            if (!resultado)
                return BadRequest("No se pudo crear el profesor. Verifique los datos.");

            return Ok(new { message = "Profesor creado exitosamente." });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, UpdateProfesorRequest request)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != id.ToString())
            {
                return StatusCode(403, "No tiene permisos para modificar este profesor.");
            }

            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var resultado = _profesorService.Update(id, request);
            if (!resultado)
                return BadRequest("No se pudo actualizar el profesor. Verifique los datos.");

            return Ok(new { message = "Profesor actualizado exitosamente." });
        }
    }
}
