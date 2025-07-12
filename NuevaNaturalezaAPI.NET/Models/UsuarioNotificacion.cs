using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class UsuarioNotificacion
{
    public Guid IdUsuarioNotificacion { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdNotificacion { get; set; }

    public bool? Leido { get; set; }
}
