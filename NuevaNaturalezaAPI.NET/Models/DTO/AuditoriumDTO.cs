using NuevaNaturalezaAPI.NET.Models.DB;
using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class AuditoriumDTO
{
    public Guid IdAuditoria { get; set; } = Guid.NewGuid();

    public Guid IdUsuario { get; set; }

    public Guid IdDispositivo { get; set; }

    public Guid IdAccion { get; set; }

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;

    public string? UsuarioNombre { get; set; } = null!;

    public string? DispositivoNombre { get; set; }

    public string? AccionNombre { get; set; }

    public int Estado { get; set; }

    public DispositivoDTO? IdDispositivoNavigation { get; set; }
    public virtual UsuarioDTO? IdUsuarioNavigation { get; set; } = null!;
    public virtual AccionActDTO? IdAccionNavigation { get; set; } = null!;



}
