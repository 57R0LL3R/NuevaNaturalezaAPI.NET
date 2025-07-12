using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class TipoUnidadMedidum
{
    public Guid IdTipoUnidadMedida { get; set; }

    public Guid? IdTipoMedicion { get; set; }

    public Guid? IdUnidadMedida { get; set; }
}
