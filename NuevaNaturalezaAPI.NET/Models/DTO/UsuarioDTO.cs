namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class UsuarioDTO
    {
        public Guid IdUsuario { get; set; } = Guid.NewGuid();

        public Guid IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Clave { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Codigo { get; set; } = null!;

    }
}
