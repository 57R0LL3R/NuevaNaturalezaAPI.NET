using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class EstadoDispositivo
{
    public Guid IdEstadoDispositivo { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();
}
