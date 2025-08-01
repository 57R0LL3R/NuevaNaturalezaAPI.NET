﻿using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class MedicionDTO
{
    public Guid IdMedicion { get; set; } = Guid.NewGuid();

    public Guid IdSensor { get; set; }

    public Guid IdFechaMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public Guid IdEstadoDispositivo { get; set; }

    public double Valor { get; set; }
}
