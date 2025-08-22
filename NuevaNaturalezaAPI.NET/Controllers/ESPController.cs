using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ESPController(IESPService service) : ControllerBase
    {
        private readonly IESPService _service = service;

        [HttpPost("Medidas")]
        public async Task<IActionResult> Data(List<MedicionDTO> mediciones)
        {
            return Ok(await _service.UpdateMedicions(mediciones));
        }
        [HttpGet("Estados")]
        public async Task<IActionResult> Estados()
        {
            return Ok(await _service.GetActuadores());
        }
    }
}
