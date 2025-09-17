using NuevaNaturalezaAPI.NET.Models.DB;

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class UsuarioDTO
    {
        public Guid IdUsuario { get; set; } = Guid.NewGuid();

        public Guid IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string? Clave { get; set; } = "";

        public string Cedula { get; set; } = null!;

        public string? Codigo { get; set; } = null!;
        public virtual RolDTO? IdRolNavigation { get; set; } = null!;

    }
}
