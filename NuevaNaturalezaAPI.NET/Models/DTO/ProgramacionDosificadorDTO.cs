namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class ProgramacionDosificadorDTO
    {
        public Guid IdProgramacion { get; set; } = Guid.NewGuid();
        public Guid IdDosificador { get; set; }
        public int Hora { get; set; }
        public int Minuto { get; set; }
        public int TiempoSegundos { get; set; }
        public string? NombreDosificador { get; set; }

    }
}
