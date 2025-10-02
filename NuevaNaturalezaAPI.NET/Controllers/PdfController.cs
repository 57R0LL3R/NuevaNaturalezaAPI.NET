using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController(IPdfService pdfservice) : ControllerBase
    {
        private readonly IPdfService _pdfService= pdfservice;

        [HttpPost("Sen")]
        public async Task< IActionResult> PdfSens(PdfQuery pdfSensores) { 
            
            return Ok(await _pdfService.PdfSensor(pdfSensores));
        }
        [HttpPost("Audi")]
        public async Task<IActionResult> PdfAudi(PdfQuery pdfSensores)
        {

            return Ok(await _pdfService.PdfAuditoria(pdfSensores));
        }
        [HttpPost("Even")]
        public async Task<IActionResult> PdfEven(PdfQuery pdfSensores)
        {

            return Ok(await _pdfService.PdfEvento(pdfSensores));
        }
    }
}
