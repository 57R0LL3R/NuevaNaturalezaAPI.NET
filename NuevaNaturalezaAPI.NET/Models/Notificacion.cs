using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Notificacion
{
    public Guid IdNotificacion { get; set; }

    public Guid IdTitulo { get; set; }

    public Guid IdTipoNotificacion { get; set; }

    public string? Mensaje { get; set; }

    public string? Enlace { get; set; }

    public virtual TipoNotificacion IdTipoNotificacionNavigation { get; set; } = null!;

    public virtual Titulo IdTituloNavigation { get; set; } = null!;
}
