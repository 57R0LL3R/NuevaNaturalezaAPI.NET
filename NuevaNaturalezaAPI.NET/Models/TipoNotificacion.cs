using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class TipoNotificacion
{
    public Guid IdTipoNotificacion { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
