namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class PdfQuery
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string[]? IdsDispositivos { get; set; }
    }
}
