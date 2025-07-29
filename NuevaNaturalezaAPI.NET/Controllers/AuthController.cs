using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        private readonly IAuthService _loginService = service;

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            return Ok(await _loginService.Login(login));
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UsuarioDTO usuario)
        {
            return Ok(await _loginService.Register(usuario));
        }
        [HttpPost("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            return Ok(await _loginService.LogOut());
        }
    }
}
