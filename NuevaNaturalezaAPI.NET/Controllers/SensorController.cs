using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {

        private readonly ISensorService _sensorService;

        public SensorController(ISensorService SensorService)
        {
            _sensorService = SensorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDTO>>> Get()
        {
            return Ok(await _sensorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDTO>> Get(Guid id)
        {
            var result = await _sensorService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SensorDTO>> Post(SensorDTO dto)
        {
            var created = await _sensorService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdSensor }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, SensorDTO dto)
        {
            var updated = await _sensorService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _sensorService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
