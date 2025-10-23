namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Area
    {

        public Guid IdArea { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();
    }
}
