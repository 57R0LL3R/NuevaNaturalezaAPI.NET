using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class TipoMUnidadM
{
    public Guid IdTipoMUnidadM { get; set; }

    public Guid IdTipoMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public virtual TipoMedicion IdTipoMedicionNavigation { get; set; } = null!;

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;
}
