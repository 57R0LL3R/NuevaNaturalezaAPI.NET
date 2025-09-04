using NuevaNaturalezaAPI.NET.Models.DB;
using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class TipoMedicionDTO
{
    public Guid IdTipoMedicion { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public virtual ICollection<TipoMUnidadM> TipoMUnidadMs { get; set; } = new List<TipoMUnidadM>();

}
