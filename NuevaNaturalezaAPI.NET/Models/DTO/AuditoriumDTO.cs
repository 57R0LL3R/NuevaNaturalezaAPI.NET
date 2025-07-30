using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class AuditoriumDTO
{
    public Guid IdAuditoria { get; set; } = Guid.NewGuid();

    public Guid IdUsuario { get; set; }

    public Guid IdDispositivo { get; set; }

    public string Accion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;
}
