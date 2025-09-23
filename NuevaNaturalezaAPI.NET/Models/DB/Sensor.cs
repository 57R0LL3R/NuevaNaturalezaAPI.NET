using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Sensor
{
    public Guid IdSensor { get; set; } = Guid.NewGuid();

    public Guid? IdDispositivo { get; set; }

    public Guid? IdTipoMUnidadM { get; set; }

    public virtual Dispositivo? IdDispositivoNavigation { get; set; } = null!;

    public virtual TipoMUnidadM? IdTipoMUnidadMNavigation { get; set; } = null!;

    public virtual ICollection<Medicion>? Medicions { get; set; } = new List<Medicion>();

    public virtual ICollection<PuntoOptimo>? PuntoOptimos { get; set; } = new List<PuntoOptimo>();
}
