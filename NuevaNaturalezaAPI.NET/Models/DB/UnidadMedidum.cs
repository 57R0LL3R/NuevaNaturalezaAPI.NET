using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class UnidadMedidum
{
    public Guid IdUnidadMedida { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;


    public virtual ICollection<TipoMUnidadM> TipoMUnidadMs { get; set; } = new List<TipoMUnidadM>();
}
