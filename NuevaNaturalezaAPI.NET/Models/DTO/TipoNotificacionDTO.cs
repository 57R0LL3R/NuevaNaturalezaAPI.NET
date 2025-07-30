using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class TipoNotificacionDTO
{
    public Guid IdTipoNotificacion { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

}
