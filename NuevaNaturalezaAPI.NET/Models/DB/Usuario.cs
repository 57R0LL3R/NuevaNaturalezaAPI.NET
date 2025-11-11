using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public class Usuario
{
    public Guid IdUsuario { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public Guid IdRol { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual Rol IdRolNavigation { get; set; } = null!;
    public virtual ICollection<Checklist> Checklists { get; set; } = new List<Checklist>();
}
