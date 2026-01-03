using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ESPController(IESPService service, ISyncQueue queue) : ControllerBase
    {

        private readonly ISyncQueue _syncQueue = queue;
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
        [HttpPost("Sincronizar")]
        public IActionResult Sincronizar(List<List<Dictionary<string, object>>>? dSensores)
        {
            _syncQueue.Enqueue(new SyncJob
            {
                Sensores = dSensores ?? []
            });

            return Ok(new
            {
                status = 200,
                message = "Datos recibidos"
            });
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


        [HttpGet("Confirm2")]
        public async Task<IActionResult> Confirm2([FromQuery] string estadosf)
        {
            return Ok(await _service.Confirm2(estadosf));
        }
    }
}
