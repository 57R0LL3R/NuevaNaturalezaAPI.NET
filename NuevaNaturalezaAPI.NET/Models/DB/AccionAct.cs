namespace NuevaNaturalezaAPI.NET.Models.DB;
enum AccionActenum
{
    Min, Max
}

public class AccionAct
{
    public Guid IdAccionAct { get; set; } = Guid.NewGuid();
    public string Accion { get; set; }
    public virtual ICollection<Actuador> Actuadores { get; set; } = new List<Actuador>();

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    public virtual ICollection<ExcesoPuntoOptimo> ExcesoPuntoOptimo { get; set; }
}
