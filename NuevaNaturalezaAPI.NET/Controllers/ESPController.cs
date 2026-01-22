using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ESPController(IESPService service, ISyncQueue queue) : ControllerBase
    {

        private readonly ISyncQueue _syncQueue = queue;
        private readonly IESPService _service = service;

        [HttpPost("Medidas")]
        public IActionResult Data(List<MedicionesESP> medicion)
        {
            List<List<Dictionary<string, object>>> dSensores = [];
            foreach (var kval in medicion)
            {
                if (kval.DatosSensores == null) continue;

                if (kval.Fecha != null) {
                    kval.DatosSensores.Last().Add("fecha", kval.Fecha);
                }
                else
                {
                    // 2026 - 01 - 16T21: 28:03
                    DateTime date = DateTime.UtcNow.AddHours(-5);
                    kval.DatosSensores.Last().Add("fecha", date.ToUniversalTime());
                }

                dSensores.Add(kval.DatosSensores);

            }

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
        public async Task<IActionResult> Confirm([FromQuery] string estadosf)
        {
            return Ok(await _service.Confirm(estadosf));
        }


        [HttpGet("Confirm2")]
        public async Task<IActionResult> Confirm2([FromQuery] string estadosf)
        {
            return Ok(await _service.Confirm2(estadosf));
        }

        [HttpGet("LMU")]
        public async Task<IActionResult> LMU()
        {
            return Ok(await _service.LMU());
        }
    }
}
