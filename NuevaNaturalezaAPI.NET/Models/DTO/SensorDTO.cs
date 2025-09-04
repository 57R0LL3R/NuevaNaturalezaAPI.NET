using NuevaNaturalezaAPI.NET.Models.DB;
using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class SensorDTO
{
    public Guid IdSensor { get; set; } = Guid.NewGuid();

    public Guid IdDispositivo { get; set; }

    public Guid IdTipoMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public virtual TipoMedicion? IdTipoMedicionNavigation { get; set; } = null!;

    public virtual ICollection<Medicion>? Medicions { get; set; } = new List<Medicion>();

    public virtual ICollection<PuntoOptimo>? PuntoOptimos { get; set; } = new List<PuntoOptimo>();
}
