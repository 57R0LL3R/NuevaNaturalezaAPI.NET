using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Titulo
{
    public Guid IdTitulo { get; set; }

    public string Titulo1 { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
