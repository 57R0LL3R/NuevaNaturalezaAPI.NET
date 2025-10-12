using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Dispositivo
{
    public Guid IdDispositivo { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;
    public string? SegundoNombre { get; set; } = null!;

    public string? Sn { get; set; }

    public string? Descripcion { get; set; }

    public string? Image { get; set; }

    public Guid? IdTipoDispositivo { get; set; }

    public Guid? IdSistema { get; set; }

    public Guid? IdMarca { get; set; }

    public Guid? IdEstadoDispositivo { get; set; }

    public virtual EstadoDispositivo IdEstadoDispositivoNavigation { get; set; } = null!;

    public virtual ICollection<Actuador> Actuadores { get; set; } = new List<Actuador>();

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Sistema? IdSistemaNavigation { get; set; }

    public virtual TipoDispositivo? IdTipoDispositivoNavigation { get; set; }

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

    public virtual ICollection<ChecklistDetalle> ChecklistDetalles { get; set; } = new List<ChecklistDetalle>();

    // en Models/DB/Dispositivo.cs (dentro de la clase Dispositivo)
    public virtual ICollection<ProgramacionDosificador>? ProgramacionDosificadores { get; set; }

    public virtual ICollection<Dosificador>? Dosificadores { get; set; } = new List<Dosificador>();

}
