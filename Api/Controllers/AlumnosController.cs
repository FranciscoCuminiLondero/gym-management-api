using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;
        
        public AlumnosController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpGet]
        public ActionResult<List<AlumnoResponse>> GetAll()
        {
            var alumnos = _alumnoService.GetAll();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public ActionResult<AlumnoResponse> GetById([FromRoute] int id)
        {
            var alumno = _alumnoService.GetById(id);
            if (alumno == null) {
                return NotFound();
            }
            return Ok(alumno);
        }

        [HttpPost]
        public ActionResult<bool> Create(CreateAlumnoRequest request)
        {
            if (request == null) {
                return BadRequest("La solicitud no puede ser nula");
            }
            var resultado = _alumnoService.Create(request);

            if(!resultado)
            {
                return BadRequest("No se pudo crear el alumno. Verifique los datos (DNI duplicado o campos incompletos).");
            }

            return Ok(resultado);
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
