using Microsoft.AspNetCore.SignalR;

namespace NuevaNaturalezaAPI.NET.Utilities
{

    public class SignalRHub : Hub
    {
        public async Task EnviarNotificacion(string mensaje)
        {
            await Clients.All.SendAsync("ReceiveUpdate", mensaje);
        }
    }
}
