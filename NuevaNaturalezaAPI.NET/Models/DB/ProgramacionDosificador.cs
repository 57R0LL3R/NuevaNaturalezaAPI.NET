using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class ProgramacionDosificador
    {
        [Key]
        public Guid IdProgramacion { get; set; } = Guid.NewGuid();

        public Guid IdDosificador { get; set; }
        public int Hora { get; set; }
        public int Minuto { get; set; }
        public int TiempoSegundos { get; set; }

        public virtual Dosificador Dosificador { get; set; } = null!;
    }
}
