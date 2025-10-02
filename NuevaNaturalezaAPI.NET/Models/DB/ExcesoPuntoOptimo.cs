namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class ExcesoPuntoOptimo
    {
        public Guid IdExcesoPuntoOptimo { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public Guid? IdAccionAct { get; set; }
        public Guid? IdPuntoOptimo { get; set; }
        public Guid? IdTipoExceso { get; set; }
        public virtual Dispositivo? IdDispositivoNavigation { get; set; }
        public virtual AccionAct? IdAccionActNavigation { get; set; }
        public virtual PuntoOptimo? IdPuntoOptimoNavigation { get; set; }
        public virtual TipoExceso? IdTipoExcesoNavigation { get; set; }

    }
}
