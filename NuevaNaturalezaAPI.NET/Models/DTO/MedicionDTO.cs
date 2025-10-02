using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class MedicionDTO
{
    public Guid IdMedicion { get; set; } = Guid.NewGuid();

    public Guid IdSensor { get; set; }

    public Guid IdFechaMedicion { get; set; }

    public Guid? IdTipoMUnidadM { get; set; }

    public double Valor { get; set; }
    public DateTime Fecha { get; set; }
}
