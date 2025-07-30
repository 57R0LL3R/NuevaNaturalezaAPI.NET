﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        private readonly IAuthService _authService = service;

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            return Ok(await _authService.Login(login));
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UsuarioDTO usuario)
        {
            return Ok(await _authService.Register(usuario));
        }
        [HttpPost("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            return Ok(await _authService.LogOut());
        }
    }
}
