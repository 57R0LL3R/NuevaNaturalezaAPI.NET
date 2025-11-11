using System;

namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class ChecklistDetalle
    {
        public Guid IdDetalle { get; set; } = Guid.NewGuid();

        public Guid IdChecklist { get; set; }
        public Guid IdDispositivo { get; set; }   // sensor o actuador

        public string Tipo { get; set; } = string.Empty; // "Sensor" o "Actuador"
        public string ValorRegistrado { get; set; } = string.Empty; // ej: "7.2 pH", "Encendido", etc.
        public string? UltimoValorMedido { get; set; } = string.Empty; // ej: "7.2 pH", "Encendido", etc.

        // 🔹 Relaciones
        public virtual Checklist Checklist { get; set; } = null!;
        public virtual Dispositivo IdDispositivoNavigation { get; set; } = null!;
    }
}
