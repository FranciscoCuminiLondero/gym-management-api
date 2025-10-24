using Application.Abstractions;
using Application.Services;
using Contract.Requests;
using Contract.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        public AuthController(
            IAuthService authService,
            IConfiguration configuration,
            IUsuarioRepository usuarioRepository)
        {
            _authService = authService;
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email y contraseña son obligatorios.");

            if (request.Role == "Profesor" || request.Role == "Administrador")
            {
                if (!User.Identity?.IsAuthenticated ?? true)
                    return Unauthorized("Debe estar autenticado como administrador para crear profesores o administradores.");

                if (!User.IsInRole("Administrador"))
                    return StatusCode(403, "Solo los administradores pueden crear profesores o administradores.");
            }

            if (request.PlanId <= 0 && request.Role == "Alumno")
                return BadRequest("Debe seleccionar un plan válido.");

            var authResult = _authService.Register(request);
            if (authResult == null)
            {
                if (_usuarioRepository.ExistsByEmail(request.Email))
                    return BadRequest("Ya existe una cuenta con ese email.");

                return BadRequest("No se pudo crear la cuenta. Verifique los datos e intente nuevamente.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authResult.Id.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, authResult.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                role = authResult.Role,
                id = authResult.Id,
                nombre = authResult.Nombre
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResponse = _authService.Login(request);
            if (authResponse == null)
                return Unauthorized("Credenciales incorrectas.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authResponse.Id.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, authResponse.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                role = authResponse.Role,
                id = authResponse.Id,
                nombre = authResponse.Nombre
            });
        }
    }
}
