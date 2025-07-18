using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Sensor
{
    public Guid IdSensor { get; set; }

    public Guid IdDispositivo { get; set; }

    public Guid IdTipoMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public virtual Dispositivo IdDispositivoNavigation { get; set; } = null!;

    public virtual TipoMedicion IdTipoMedicionNavigation { get; set; } = null!;

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;

    public virtual ICollection<Medicion> Medicions { get; set; } = new List<Medicion>();

    public virtual ICollection<PuntoOptimo> PuntoOptimos { get; set; } = new List<PuntoOptimo>();
}
