using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class NotificacionDTO
{
    public Guid IdNotificacion { get; set; } = Guid.NewGuid();

    public Guid IdTitulo { get; set; }

    public Guid IdTipoNotificacion { get; set; }

    public string? Mensaje { get; set; }

    public string? Enlace { get; set; }
    public bool? Leido { get; set; }
}
