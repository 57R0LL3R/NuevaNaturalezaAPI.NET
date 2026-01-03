namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class SyncJob
    {
        public List<List<Dictionary<string, object>>> Sensores { get; set; } = [];
        public DateTime FechaRecepcion { get; set; } = DateTime.UtcNow;
    }

}
