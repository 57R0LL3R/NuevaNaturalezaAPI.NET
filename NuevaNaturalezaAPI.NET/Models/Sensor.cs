using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Sensor
{
    public Guid IdSensor { get; set; }

    public Guid? IdDispositivo { get; set; }

    public Guid? IdTipoMedicion { get; set; }
}
