using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class EventoDTO
{
    public Guid IdEvento { get; set; } = Guid.NewGuid();

    public Guid IdDispositivo { get; set; }

    public Guid IdImpacto { get; set; }

    public Guid IdSistema { get; set; }

    public DateTime FechaEvento { get; set; }
}
