using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public partial class TipoMUnidadMDTO
{
    public Guid IdTipoMUnidadM { get; set; } = Guid.NewGuid();

    public Guid IdTipoMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }
}
