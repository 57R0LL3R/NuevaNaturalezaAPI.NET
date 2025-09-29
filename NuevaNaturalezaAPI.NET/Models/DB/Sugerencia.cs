namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Sugerencia
    {
        public Guid IdSugerencia { get; set; } = Guid.NewGuid();
        public string? Usuario { get; set; }
        public string? Correo { get; set; }
        public string Mensaje { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
