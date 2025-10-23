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
        public async Task<IActionResult> Data(List<MedicionesESP> medicion)
        {
            Response resf = new();
            foreach (var med in medicion)
            {
               var res = await _service.UpdateMedicions(med);
                resf = res;
                if (res.NumberResponse == (int)NumberResponses.Error){
                
                    break;

                }
                
            }
            return resf.NumberResponse==(int)NumberResponses.Correct ? Ok(resf): BadRequest(resf);
        }

        [HttpGet("Estados")]
        public async Task<IActionResult> Estados()
        {
            return Ok(await _service.GetOutsOfActuators());
        }  

        [HttpGet("Confirm")]
        public async Task<IActionResult> Confirm([FromQuery]string estadosf)
        {
            return Ok(await _service.Confirm(estadosf));
        }
    }
}
