using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.DTOs
{
    public class ChecklistDTO
    {
        public Guid IdChecklist { get; set; } = Guid.NewGuid();
        public DateTime Fecha { get; set; }
        public string? ObservacionGeneral { get; set; }


        public string? Usuario { get; set; } = string.Empty;   // usuario que hizo el checklist
        public Guid? IdUsuario { get; set; } 
        public virtual UsuarioDTO? IdUsuarioNavigation { get; set; }

        public List<ChecklistDetalleDTO> Detalles { get; set; } = new();
    }

    public class ChecklistDetalleDTO
    {
        public Guid IdChecklistDetalle { get; set; } = Guid.NewGuid();
        public Guid IdDispositivo { get; set; }
        public string NombreDispositivo { get; set; } = string.Empty;

        public string? ValorRegistrado { get; set; }  // lo que escribió el usuario
        public string? UltimoValorMedido { get; set; } = string.Empty; // ej: "7.2 pH", "Encendido", etc.
        public bool? EstadoActuador { get; set; } // "ON" / "OFF" si aplica
    }
}

