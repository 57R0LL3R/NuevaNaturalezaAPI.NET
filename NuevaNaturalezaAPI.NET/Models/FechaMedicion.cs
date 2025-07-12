using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class FechaMedicion
{
    public Guid IdFechaMedicion { get; set; }

    public DateTime? FechaHora { get; set; }
}
