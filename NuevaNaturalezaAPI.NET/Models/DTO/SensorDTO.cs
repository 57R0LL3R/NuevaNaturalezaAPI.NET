using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class SensorDTO
{
    public Guid IdSensor { get; set; } = Guid.NewGuid();

    public Guid IdDispositivo { get; set; }

    public Guid IdTipoMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }
}
