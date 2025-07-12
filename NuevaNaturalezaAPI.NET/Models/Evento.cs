using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Evento
{
    public Guid IdEvento { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? Fecha { get; set; }

    public Guid? IdImpacto { get; set; }

    public Guid? IdSistema { get; set; }

    public Guid? IdTitulo { get; set; }
}
