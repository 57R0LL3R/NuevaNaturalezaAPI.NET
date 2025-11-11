namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Actuador
    {
        public Guid IdActuador { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public Guid? IdAccionAct { get; set; }
        public string? On {  get; set; } 
        public string? Off { get; set; }

        public virtual AccionAct? IdAccionActNavigation { get; set; } 
        public virtual Dispositivo? IdDispositivoNavigation { get; set; }
        public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();
    }
}
