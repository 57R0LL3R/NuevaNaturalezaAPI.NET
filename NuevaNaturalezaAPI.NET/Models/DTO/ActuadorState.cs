namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class ActuadorState
    {
        public Guid id { get; set; }
        public ActuadorDTO dto { get; set; }
        public Guid? idSistema { get; set; }
        public string? observacion { get; set; }

    }
}
