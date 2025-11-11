using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Evento
{
    public Guid IdEvento { get; set; } = Guid.NewGuid();

    public Guid IdDispositivo { get; set; }

    public Guid IdImpacto { get; set; }

    public Guid IdSistema { get; set; }
    public Guid? IdAccionAct { get; set; }

    public DateTime FechaEvento { get; set; } = DateTime.UtcNow;

    public virtual AccionAct? IdAccionActNavigation { get; set; }


    public virtual Dispositivo IdDispositivoNavigation { get; set; } = null!;

    public virtual Impacto IdImpactoNavigation { get; set; } = null!;

    public virtual Sistema IdSistemaNavigation { get; set; } = null!;
}
