using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class UnidadMedidum
{
    public Guid IdUnidadMedida { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Medicion> Medicions { get; set; } = new List<Medicion>();

    public virtual ICollection<PuntoOptimo> PuntoOptimos { get; set; } = new List<PuntoOptimo>();

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

    public virtual ICollection<TipoMUnidadM> TipoMUnidadMs { get; set; } = new List<TipoMUnidadM>();
}
