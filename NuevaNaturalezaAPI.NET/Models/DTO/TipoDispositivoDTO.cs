using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class TipoDispositivoDTO
{
    public Guid IdTipoDispositivo { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

}
