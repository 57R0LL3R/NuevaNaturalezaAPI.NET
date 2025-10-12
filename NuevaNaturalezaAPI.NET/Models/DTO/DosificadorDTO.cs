using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class DosificadorDTO
    {
        public Guid IdDosificador { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public string? LetraActivacion { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<ProgramacionDosificadorDTO>? Programaciones { get; set; }
    }
}
