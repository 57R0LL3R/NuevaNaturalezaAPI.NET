using NuevaNaturalezaAPI.NET.Models.DB;

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class AreaDTO
    {
        public Guid IdArea { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
    }
}
