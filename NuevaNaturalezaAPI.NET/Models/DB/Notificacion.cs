using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Notificacion
{
    public Guid IdNotificacion { get; set; } = Guid.NewGuid();

    public Guid IdTitulo { get; set; }

    public Guid IdTipoNotificacion { get; set; }

    public string? Mensaje { get; set; }

    public string? Enlace { get; set; }

    public virtual TipoNotificacion IdTipoNotificacionNavigation { get; set; } = null!;

    public virtual Titulo IdTituloNavigation { get; set; } = null!;
}
