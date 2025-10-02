namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class SugerenciaDTO
    {
        public Guid IdSugerencia { get; set; } = Guid.NewGuid();
        public string? Usuario { get; set; }
        public string? Correo { get; set; }
        public string Mensaje { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        
    }
}
