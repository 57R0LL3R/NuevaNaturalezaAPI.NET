using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMedicionController(ITipoMedicionService tipoMedicionService) : ControllerBase
    {
        private readonly ITipoMedicionService _tipoMedicionService = tipoMedicionService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMedicionDTO>>> Get()
        {
            return Ok(await _tipoMedicionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMedicionDTO>> Get(Guid id)
        {
            var result = await _tipoMedicionService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TipoMedicionDTO>> Post(TipoMedicionDTO dto)
        {
            var created = await _tipoMedicionService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdTipoMedicion }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TipoMedicionDTO dto)
        {
            var updated = await _tipoMedicionService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _tipoMedicionService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

