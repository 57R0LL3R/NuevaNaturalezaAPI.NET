using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class PuntoOptimo
{
    public Guid IdPuntoOptimo { get; set; } = Guid.NewGuid();

    public Guid? IdSensor { get; set; }

    public Guid IdTipoMUnidadM { get; set; }

    public double ValorMin { get; set; }

    public double ValorMax { get; set; }

    public virtual Sensor IdSensorNavigation { get; set; } = null!;

    public virtual TipoMUnidadM? IdTipoMUnidadMNavigation { get; set; } = null!;

}
