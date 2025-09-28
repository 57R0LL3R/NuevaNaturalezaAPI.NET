using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class FechaMedicion
{
    public Guid IdFechaMedicion { get; set; } = Guid.NewGuid();

    public DateTime Fecha { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
    TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));


    public virtual ICollection<Medicion> Medicions { get; set; } = new List<Medicion>();
}
