using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AuthService(NuevaNatuContext context,IMapper mapper,IEmailService _serviceEmail) : IAuthService
    {
        private readonly IMapper _mapper = mapper;
        private readonly NuevaNatuContext _context = context;

        public async Task<Response> Login(LoginModel loginModel)
        {
            var PASS = Hash256.Hash(loginModel.Pass);
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == loginModel.User && x.Clave == Hash256.Hash(loginModel.Pass));
            if (user == null) 
            {
                return new();
            }
            //Logica de jwt
            return new (){NumberResponse=(int)NumberResponses.Correct,Data=user };
        }

        public Task<Response> LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Register(UsuarioDTO registerModel)
        {

            var user = _mapper.Map<Usuario>(registerModel);
            if (user == null)
            {
                return new();
            }
            user.IdUsuario = Guid.NewGuid();
            var result=await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
            Response response = new() { NumberResponse = (int)NumberResponses.Correct, Data = user };
            return response;
        }


        public async Task<Response> Recover(LoginModel lModel)
        {

            var user = await _context.Usuarios.FirstOrDefaultAsync(x=>x.Correo==lModel.User);
            if (user == null || lModel.Url is null)
            {
                return new();
            }
            var rcontrasena = new RecuperarContrasena { Correo = lModel.User };
            _context.RecuperarContrasena.Add(rcontrasena);

            await _context.SaveChangesAsync();
            await _serviceEmail.SendEmailAsync(lModel.User,"Solicitud de recuperacion de contraseña",
                $"<h1><strong>Solicitud de recuperacion de contraseña</strong></h1><br><h4>Para recuperar su contraseña debe de acceder a este link: </h4>" +
                $"<strong><a href=\"{lModel.Url}?id={rcontrasena.IdRecuperarContrasena}\">Recuperar contraseña</a></strong>");
            Response response = new() { NumberResponse = (int)NumberResponses.Correct, Data = lModel ,Message="El mensaje fue enviado a su correo electronico"};
            return response;
        }

        public async Task<Response> Recover(string id, LoginModel lModel)
        {
            var rContrasena = await _context.RecuperarContrasena.FirstOrDefaultAsync(x => x.IdRecuperarContrasena == Guid.Parse(id) && x.Status==(int)NumberStatus.Error);
            if (rContrasena == null)
            {
                return new Response();
            }
            var usuario = await _context.Usuarios.FirstAsync(x => x.Correo == rContrasena.Correo);
            rContrasena.Status=(int)NumberStatus.Correct;
            _context.Entry(rContrasena).State = EntityState.Modified;
            usuario.Clave = Hash256.Hash(lModel.Pass);
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                Data = usuario
            };
        }
    }
}
