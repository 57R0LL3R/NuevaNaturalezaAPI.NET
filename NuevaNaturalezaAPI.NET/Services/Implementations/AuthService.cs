using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly NuevaNatuContext _context;
        private readonly IEmailService _serviceEmail;
        private readonly IConfiguration _configuration; // 🔹 para leer el Jwt:Key y Jwt:Issuer
        private readonly IHttpContextAccessor _httpContextAccessor; // 🔹 para manipular cookies

        public AuthService(
            NuevaNatuContext context,
            IMapper mapper,
            IEmailService serviceEmail,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _serviceEmail = serviceEmail;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        // 🔹 LOGIN CON JWT Y COOKIE
        public async Task<Response> Login(LoginModel loginModel)
        {
            var hashedPass = Hash256.Hash(loginModel.Pass);

            var user = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(x => x.Correo == loginModel.User && x.Clave == hashedPass);

            if (user == null)
            {
                return new Response
                {
                    NumberResponse = (int)NumberResponses.Incorrect,
                    Message = "Credenciales incorrectas"
                };
            }

            // 🔹 Crear los claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, user.Correo),
                new Claim(ClaimTypes.Role, user.IdRolNavigation.Nombre) // Asegúrate de que Rol tenga NombreRol
            };

            // 🔹 Crear el token JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // 🔹 Guardar el token en una cookie segura
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddHours(2)
            };

            _httpContextAccessor.HttpContext!.Response.Cookies.Append("jwt", jwt, cookieOptions);

            return new Response
            {
                NumberResponse = (int)NumberResponses.Correct,
                Message = "Inicio de sesión exitoso",
                Data = new
                {
                    user.IdUsuario,
                    user.Nombre,
                    Rol = user.IdRolNavigation.Nombre
                }
            };
        }

        // 🔹 CERRAR SESIÓN (BORRAR COOKIE)
        public Task<Response> LogOut()
        {
            _httpContextAccessor.HttpContext!.Response.Cookies.Delete("jwt");
            return Task.FromResult(new Response
            {
                NumberResponse = (int)NumberResponses.Correct,
                Message = "Sesión cerrada correctamente"
            });
        }

        // 🔹 REGISTRO (SIN CAMBIOS)
        public async Task<Response> Register(UsuarioDTO registerModel)
        {
            var user = _mapper.Map<Usuario>(registerModel);
            if (user == null)
            {
                return new Response();
            }

            user.IdUsuario = Guid.NewGuid();
            var result = await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();

            return new Response
            {
                NumberResponse = (int)NumberResponses.Correct,
                Data = user
            };
        }

        // 🔹 RECUPERAR CONTRASEÑA
        public async Task<Response> Recover(LoginModel lModel)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == lModel.User);
            if (user == null || string.IsNullOrEmpty(lModel.Url))
            {
                return new Response();
            }

            var rcontrasena = new RecuperarContrasena { Correo = lModel.User };
            _context.RecuperarContrasena.Add(rcontrasena);
            await _context.SaveChangesAsync();

            await _serviceEmail.SendEmailAsync(
                lModel.User,
                "Solicitud de recuperación de contraseña",
                $"<h1><strong>Solicitud de recuperación de contraseña</strong></h1><br><h4>Para recuperar su contraseña, acceda a este enlace:</h4>" +
                $"<strong><a href=\"{lModel.Url}?id={rcontrasena.IdRecuperarContrasena}\">Recuperar contraseña</a></strong>"
            );

            return new Response
            {
                NumberResponse = (int)NumberResponses.Correct,
                Data = lModel,
                Message = "El mensaje fue enviado a su correo electrónico"
            };
        }

        public async Task<Response> Recover(string id, LoginModel lModel)
        {
            var rContrasena = await _context.RecuperarContrasena.FirstOrDefaultAsync(
                x => x.IdRecuperarContrasena == Guid.Parse(id) && x.Status == (int)NumberStatus.InProcces
            );

            if (rContrasena == null)
            {
                return new Response();
            }

            var usuario = await _context.Usuarios.FirstAsync(x => x.Correo == rContrasena.Correo);
            rContrasena.Status = (int)NumberStatus.Correct;
            _context.Entry(rContrasena).State = EntityState.Modified;
            usuario.Clave = Hash256.Hash(lModel.Pass);
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new Response { Data = usuario };
        }
    }
}
