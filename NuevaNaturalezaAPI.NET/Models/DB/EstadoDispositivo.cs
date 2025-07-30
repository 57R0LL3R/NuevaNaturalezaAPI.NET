using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class EstadoDispositivo
{
    public Guid IdEstadoDispositivo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Medicion> Medicions { get; set; } = new List<Medicion>();
}
