using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<AuthResponse> Register(RegisterRequest request)
        {
            var result = _authService.Register(request);
            if (result == null) return BadRequest("Registro fallido. Email duplicado o datos inválidos.");
            return Ok(result);
        }

        [HttpPost("login")]
        public ActionResult<AuthResponse> Login(LoginRequest request)
        {
            var result = _authService.Login(request);
            if (result == null) return Unauthorized("Credenciales incorrectas.");
            return Ok(result);
        }
    }
}
