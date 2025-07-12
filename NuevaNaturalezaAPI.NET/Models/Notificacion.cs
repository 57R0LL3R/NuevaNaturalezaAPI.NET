using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Notificacion
{
    public Guid IdNotificacion { get; set; }

    public Guid? IdEvento { get; set; }

    public Guid? IdTipoNotificacion { get; set; }

    public DateTime? FechaEnvio { get; set; }
}
