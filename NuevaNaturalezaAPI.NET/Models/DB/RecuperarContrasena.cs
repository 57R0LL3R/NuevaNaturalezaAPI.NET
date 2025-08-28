enum NumberStatus
{
    Correct, InProcces
}

namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class RecuperarContrasena
    {
        public Guid IdRecuperarContrasena { get; set; } = Guid.NewGuid();
        public string Correo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int Status { get; set; } = (int)NumberStatus.InProcces;
    }
}
