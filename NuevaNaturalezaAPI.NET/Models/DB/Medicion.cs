﻿using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Medicion
{
    public Guid IdMedicion { get; set; }

    public Guid IdSensor { get; set; }

    public Guid IdFechaMedicion { get; set; }

    public Guid IdUnidadMedida { get; set; }

    public Guid IdEstadoDispositivo { get; set; }

    public double Valor { get; set; }

    public virtual EstadoDispositivo IdEstadoDispositivoNavigation { get; set; } = null!;

    public virtual FechaMedicion IdFechaMedicionNavigation { get; set; } = null!;

    public virtual Sensor IdSensorNavigation { get; set; } = null!;

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;
}
