using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Response> Login(LoginModel loginModel);
        public Task<Response> Register(UsuarioDTO registerModel);
        public Task<Response> LogOut();

        public Task<Response> Recover(LoginModel lModel);
    }
}
