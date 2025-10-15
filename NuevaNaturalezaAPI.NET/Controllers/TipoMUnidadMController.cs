using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMUnidadMController : ControllerBase
    {
        private readonly ITipoMUnidadMService _TipoMUnidadMService;

        public TipoMUnidadMController(ITipoMUnidadMService TipoMUnidadMService)
        {
            _TipoMUnidadMService = TipoMUnidadMService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMUnidadMDTO>>> Get()
        {
            return Ok(await _TipoMUnidadMService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMUnidadMDTO>> Get(Guid id)
        {
            var result = await _TipoMUnidadMService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TipoMUnidadMDTO>> Post(TipoMUnidadMDTO dto)
        {
            var created = await _TipoMUnidadMService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdTipoMUnidadM }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TipoMUnidadMDTO dto)
        {
            var updated = await _TipoMUnidadMService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _TipoMUnidadMService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

