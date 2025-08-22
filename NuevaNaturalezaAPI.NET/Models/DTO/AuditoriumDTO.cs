using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class AuditoriumDTO
{
    public Guid IdAuditoria { get; set; } = Guid.NewGuid();

    public Guid IdUsuario { get; set; }

    public Guid IdDispositivo { get; set; }

    public Guid IdAccionAct { get; set; }

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;

    public int Estado { get; set; }
    public DispositivoDTO? IdDispositivoNavigation { get; set; }
}
