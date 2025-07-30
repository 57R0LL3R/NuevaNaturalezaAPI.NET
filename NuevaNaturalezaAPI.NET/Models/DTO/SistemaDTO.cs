using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class SistemaDTO
{
    public Guid IdSistema { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

}
