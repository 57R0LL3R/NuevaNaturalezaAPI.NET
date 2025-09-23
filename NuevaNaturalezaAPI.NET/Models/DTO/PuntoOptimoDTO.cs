using NuevaNaturalezaAPI.NET.Models.DB;
using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class PuntoOptimoDTO
{
    public Guid IdPuntoOptimo { get; set; } = Guid.NewGuid();

    public Guid IdSensor { get; set; }

    public Guid IdTipoMunidadM { get; set; }

    public double ValorMin { get; set; }

    public double ValorMax { get; set; }

    public virtual TipoMUnidadMDTO? IdTipoMUnidadMNavigation { get; set; } = null!;


}
