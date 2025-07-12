using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class PuntoOptimo
{
    public Guid IdPuntoOptimo { get; set; }

    public Guid? IdTipoMedicion { get; set; }

    public double? ValorMin { get; set; }

    public double? ValorMax { get; set; }
}
