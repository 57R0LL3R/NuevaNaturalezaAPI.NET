namespace NuevaNaturalezaAPI.NET.DTOs
{
    public class ChecklistDTO
    {
        public Guid IdChecklist { get; set; }
        public DateTime Fecha { get; set; }
        public string? ObservacionGeneral { get; set; }

        public List<ChecklistDetalleDTO> Detalles { get; set; } = new();
    }

    public class ChecklistDetalleDTO
    {
        public Guid IdChecklistDetalle { get; set; }
        public Guid IdDispositivo { get; set; }
        public string NombreDispositivo { get; set; } = string.Empty;

        public string? ValorIngresado { get; set; }  // lo que escribió el usuario para sensores
        public bool? EstadoActuador { get; set; } // "ON" / "OFF" si aplica
    }
}

