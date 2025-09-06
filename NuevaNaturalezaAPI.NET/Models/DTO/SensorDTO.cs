using NuevaNaturalezaAPI.NET.Models.DB;
using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class SensorDTO
{
    public Guid IdSensor { get; set; } = Guid.NewGuid();

    public Guid IdDispositivo { get; set; }

    public Guid IdTipoMUnidadM { get; set; }

    public virtual TipoMUnidadMDTO? IdTipoMUnidadMNavigation { get; set; } = null!;

    public virtual ICollection<MedicionDTO>? Medicions { get; set; } = new List<MedicionDTO>();

    public virtual ICollection<PuntoOptimoDTO>? PuntoOptimos { get; set; } = new List<PuntoOptimoDTO>();
}
