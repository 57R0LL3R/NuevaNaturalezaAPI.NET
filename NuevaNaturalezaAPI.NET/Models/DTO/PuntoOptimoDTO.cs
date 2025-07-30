using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class PuntoOptimoDTO
{
    public Guid IdPuntoOptimo { get; set; } = Guid.NewGuid();

    public Guid IdSensor { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public double ValorMin { get; set; }

    public double ValorMax { get; set; }

}
