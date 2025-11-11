namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class ExcesoPuntoOptimoDTO
    {

        public Guid IdExcesoPuntoOptimo { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public Guid? IdAccionAct { get; set; }
        public Guid? IdPuntoOptimo { get; set; }
        public Guid? IdTipoExceso { get; set; }
        public virtual AccionActDTO? IdAccionActNavigation { get; set; }
        public virtual TipoExcesoDTO? IdTipoExcesoNavigation { get; set; }
    }
}
