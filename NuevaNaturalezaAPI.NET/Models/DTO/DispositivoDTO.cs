using NuevaNaturalezaAPI.NET.Models.DB;
using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DTO;

public class DispositivoDTO
{
    public Guid IdDispositivo { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public string Sn { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Image { get; set; }

    public Guid? IdTipoDispositivo { get; set; }

    public Guid? IdSistema { get; set; }

    public Guid? IdMarca { get; set; }

    public Guid? IdEstadoDispositivo { get; set; }

    public virtual TipoDispositivoDTO? IdTipoDispositivoNavigation { get; set; }


    public virtual ICollection<ActuadorDTO> Actuadores { get; set; } = new List<ActuadorDTO>();

    public virtual ICollection<SensorDTO> Sensors { get; set; } = new List<SensorDTO>();

    public virtual ICollection<DosificadorDTO>? Dosificadores { get; set; } = new List<DosificadorDTO>();

    public virtual MarcaDTO? IdMarcaNavigation { get; set; }
}
