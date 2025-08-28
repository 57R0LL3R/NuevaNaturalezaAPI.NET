using Microsoft.AspNetCore.SignalR;

namespace NuevaNaturalezaAPI.NET.Utilities
{

    public class NotificacionesHub : Hub
    {
        public async Task EnviarNotificacion(string mensaje)
        {
            await Clients.All.SendAsync("RecibirNotificacion", mensaje);
        }
    }
}
