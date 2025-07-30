using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Auditorium
{
    public Guid IdAuditoria { get; set; }

    public Guid IdUsuario { get; set; }

    public Guid IdDispositivo { get; set; }

    public string Accion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;

    public virtual Dispositivo? IdDispositivoNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;
}
