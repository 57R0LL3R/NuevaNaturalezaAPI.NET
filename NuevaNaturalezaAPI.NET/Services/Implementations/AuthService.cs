using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AuthService(NuevaNatuContext context,IMapper mapper) : IAuthService
    {
        private readonly IMapper _mapper = mapper;
        private readonly NuevaNatuContext _context = context;
        public async Task<Response> Login(LoginDTO loginModel)
        {
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
            if(user == null)
            {
                return new();
            }
            return new() { NumberResponse = (int)NumberResponses.Correct, Data = await _context.Usuarios.AddAsync(user) };
        }
    }
}
