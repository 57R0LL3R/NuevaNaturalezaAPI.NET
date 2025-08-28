using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class TipoMedicion
{
    public Guid IdTipoMedicion { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

    public virtual ICollection<TipoMUnidadM> TipoMUnidadMs { get; set; } = new List<TipoMUnidadM>();
}
