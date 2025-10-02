using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IPdfService
    {
        public  Task<Response> PdfAuditoria(PdfQuery pdfAudi);
        public  Task<Response> PdfEvento(PdfQuery pdfEven);
        public  Task<Response> PdfSensor(PdfQuery pdfSen);
    }
}
