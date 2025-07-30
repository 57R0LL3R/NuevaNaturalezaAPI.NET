using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class TituloDTO
{
    public Guid IdTitulo { get; set; } = Guid.NewGuid();

    public string Titulo1 { get; set; } = null!;

}
