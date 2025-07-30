using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public  class FechaMedicionDTO
{
    public Guid IdFechaMedicion { get; set; } = Guid.NewGuid();

    public DateOnly Fecha { get; set; }

}
