using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public Guid? IdRol { get; set; }
}
