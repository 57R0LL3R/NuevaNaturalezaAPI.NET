using NuevaNaturalezaAPI.NET.Models.DB;

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class TipoExcesoDTO
    {
        public Guid IdTipoExceso { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
    }
}
