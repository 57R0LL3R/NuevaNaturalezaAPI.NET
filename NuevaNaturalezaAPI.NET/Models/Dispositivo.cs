using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Dispositivo
{
    public Guid IdDispositivo { get; set; }

    public string? Nombre { get; set; }

    public Guid? IdTipoDispositivo { get; set; }

    public Guid? IdEstadoDispositivo { get; set; }

    public Guid? IdSistema { get; set; }
}
