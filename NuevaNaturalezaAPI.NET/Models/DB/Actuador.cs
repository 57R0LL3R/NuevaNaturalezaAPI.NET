namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Actuador
    {
        public Guid IdActuador { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public Guid IdAccionAct { get; set; }
        public string? On {  get; set; } 
        public string? Off { get; set; }
        public virtual AccionAct AccionAct { get; set; } = new AccionAct();
        public virtual Dispositivo IdDispositivoNavigation { get; set; } = new Dispositivo();

    }
}
