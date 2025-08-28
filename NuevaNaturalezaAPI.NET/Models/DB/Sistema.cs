using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Sistema
{
    public Guid IdSistema { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
