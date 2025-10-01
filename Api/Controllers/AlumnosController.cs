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
        public ActionResult<AlumnoResponse> GetById(int id)
        {
            var alumno = _alumnoService.GetById(id);
            if (alumno == null) return NotFound();
            return Ok(alumno);
        }
    }
}
