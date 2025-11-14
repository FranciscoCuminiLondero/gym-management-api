using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public ActionResult<List<ClaseResponse>> GetAll([FromQuery] int? sucursalId)
        {
            var clases = sucursalId.HasValue
                ? _claseService.GetBySucursalId(sucursalId.Value)
                : _claseService.GetAll();
            return Ok(clases);
        }

        [HttpGet("fecha/{fecha}")]
        [Authorize]
        public ActionResult<List<ClaseResponse>> GetPorFecha(string fecha)
        {
            if (!DateOnly.TryParse(fecha, out var fechaParsed))
                return BadRequest("Formato de fecha inválido. Use YYYY-MM-DD.");

            var clases = _claseService.GetDisponiblesPorFecha(fechaParsed);
            return Ok(clases);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ClaseResponse> GetById(int id)
        {
            var clase = _claseService.GetById(id);
            if (clase == null) return NotFound();
            return Ok(clase);
        }

        [HttpPost]
        [Authorize(Roles = "Profesor,Administrador")]
        public IActionResult Create(CreateClaseRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest("El nombre de la clase es obligatorio.");

            if (request.Capacidad <= 0)
                return BadRequest("La capacidad debe ser mayor a 0.");

            if (request.DuracionMinutos <= 0)
                return BadRequest("La duración debe ser mayor a 0.");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            if (!isAdmin && userIdClaim != request.ProfesorId.ToString())
            {
                return StatusCode(403, "No tiene permisos para crear clases para otro profesor.");
            }

            var resultado = _claseService.Create(request);
            if (!resultado)
                return BadRequest("No se pudo crear la clase. Verifique los datos.");

            return Ok(new { message = "Clase creada exitosamente." });
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Profesor,Administrador")]
        public IActionResult Update(int id, [FromBody] UpdateClaseRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            var profesorIdClase = _claseService.GetProfesorIdByClaseId(id);
            if (profesorIdClase == null)
            {
                return NotFound("Clase no encontrada.");
            }

            if (!isAdmin && userIdClaim != profesorIdClase.ToString())
            {
                return StatusCode(403, "No tiene permisos para modificar clases de otro profesor.");
            }

            var resultado = _claseService.Update(id, request);
            if (!resultado)
                return BadRequest("No se pudo actualizar la clase. Verifique los datos.");

            return Ok(new { message = "Clase actualizada exitosamente." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Profesor,Administrador")]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador");

            var profesorIdClase = _claseService.GetProfesorIdByClaseId(id);
            if (profesorIdClase == null)
            {
                return NotFound("Clase no encontrada.");
            }

            if (!isAdmin && userIdClaim != profesorIdClase.ToString())
            {
                return StatusCode(403, "No tiene permisos para eliminar clases de otro profesor.");
            }

            var resultado = _claseService.Delete(id);
            if (!resultado) return NotFound("Clase no encontrada.");

            return Ok(new { message = "Clase eliminada exitosamente." });
        }
    }
}
