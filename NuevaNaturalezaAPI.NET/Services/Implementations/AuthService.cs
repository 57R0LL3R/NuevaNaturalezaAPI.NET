using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AuthService(NuevaNatuContext context,IMapper mapper) : IAuthService
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
    }
}
