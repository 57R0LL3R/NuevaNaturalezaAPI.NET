using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Checklist
    {
        public Guid IdChecklist { get; set; } = Guid.NewGuid();
        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Usuario { get; set; } = string.Empty;   // usuario que hizo el checklist
        public string? ObservacionesGenerales { get; set; }

        // 🔹 Relación con detalles
        public virtual ICollection<ChecklistDetalle> Detalles { get; set; } = new List<ChecklistDetalle>();
    }
}
