using System;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Models.DB
{
    public class Checklist
    {
        public Guid IdChecklist { get; set; } = Guid.NewGuid();
        public DateTime Fecha { get; set; } = DateTime.Now;

        public Guid? IdUsuario { get; set; } = Guid.NewGuid();
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public string? ObservacionGeneral { get; set; }

        // 🔹 Relación con detalles
        public virtual ICollection<ChecklistDetalle> Detalles { get; set; } 
    }
}
