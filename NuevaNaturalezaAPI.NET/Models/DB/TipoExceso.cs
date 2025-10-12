namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class TipoExceso
    {
        public Guid IdTipoExceso { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public virtual ICollection<ExcesoPuntoOptimo> ExcesoPuntoOptimo { get; set; }
    }
}
