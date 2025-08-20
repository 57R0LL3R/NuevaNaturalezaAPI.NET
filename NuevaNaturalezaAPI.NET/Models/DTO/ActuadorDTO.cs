

using NuevaNaturalezaAPI.NET.Models.DB;

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class ActuadorDTO
    {
        public Guid IdActuador { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public Guid IdAccionAct { get; set; }
        public virtual Dispositivo IdDispositivoNavigation { get; set; } = new Dispositivo();
    }
}
