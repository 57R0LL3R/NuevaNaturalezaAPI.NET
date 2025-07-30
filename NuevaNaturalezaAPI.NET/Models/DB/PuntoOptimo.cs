using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class PuntoOptimo
{
    public Guid IdPuntoOptimo { get; set; }

    public Guid IdSensor { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public double ValorMin { get; set; }

    public double ValorMax { get; set; }

    public virtual Sensor IdSensorNavigation { get; set; } = null!;

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;
}
