using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Auditorium
{
    public Guid IdAuditoria { get; set; } = Guid.NewGuid();

    public Guid IdUsuario { get; set; }

    public Guid IdDispositivo { get; set; }

    public Guid? IdAccion { get; set; }

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;

    public int? Estado { get; set; } = (int)NumberStatus.InProcces;

    public virtual Dispositivo? IdDispositivoNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;
    public virtual AccionAct? IdAccionNavigation { get; set; } = null!;

}
