using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public partial class MarcaDTO
{
    public Guid IdMarca { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

}
