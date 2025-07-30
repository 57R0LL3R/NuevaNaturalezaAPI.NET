using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class ImpactoDTO
{
    public Guid IdImpacto { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

}
