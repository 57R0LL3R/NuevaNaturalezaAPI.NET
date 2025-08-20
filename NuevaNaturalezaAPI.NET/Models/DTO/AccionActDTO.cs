
namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class AccionActDTO
    {

        public Guid IdAccionAct { get; set; } = Guid.NewGuid();
        public string Accion { get; set; }
    }
}
