using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class FechaMedicion
{
    public Guid IdFechaMedicion { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual ICollection<Medicion> Medicions { get; set; } = new List<Medicion>();
}
