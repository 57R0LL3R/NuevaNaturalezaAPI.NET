using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class TipoMUnidadM
{
    public Guid IdTipoMUnidadM { get; set; } = Guid.NewGuid();

    public Guid IdTipoMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }
    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

    public virtual TipoMedicion IdTipoMedicionNavigation { get; set; } = null!;

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;

    public virtual ICollection<Medicion> Medicions { get; set; } = new List<Medicion>();

    public virtual ICollection<PuntoOptimo> PuntoOptimos { get; set; } = new List<PuntoOptimo>();
}
