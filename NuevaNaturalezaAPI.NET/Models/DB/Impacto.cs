using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Impacto
{
    public Guid IdImpacto { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
