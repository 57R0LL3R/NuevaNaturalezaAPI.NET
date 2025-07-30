using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public partial class UnidadMedidumDTO
{
    public Guid IdUnidadMedida { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;
}
