using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Medicion
{
    public Guid IdMedicion { get; set; }

    public Guid? IdSensor { get; set; }

    public double? Valor { get; set; }

    public Guid? IdFechaMedicion { get; set; }
}
