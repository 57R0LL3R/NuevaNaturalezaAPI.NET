using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Auditorium
{
    public Guid IdAuditoria { get; set; }

    public Guid? IdUsuario { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Accion { get; set; }

    public string? TablaAfectada { get; set; }
}
