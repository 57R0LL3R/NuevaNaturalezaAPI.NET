using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Dosificador
    {
        public Guid IdDosificador { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public string? LetraActivacion { get; set; }
        public string? Descripcion { get; set; }

        public virtual Dispositivo? IdDispositivoNavigation { get; set; } 
        public virtual ICollection<ProgramacionDosificador> Programaciones { get; set; } = new List<ProgramacionDosificador>();
    }
}
