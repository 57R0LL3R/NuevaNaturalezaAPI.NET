using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoNotificacionController : ControllerBase
    {
        private readonly ITipoNotificacionService _TipoNotificacionService;

        public TipoNotificacionController(ITipoNotificacionService TipoNotificacionService)
        {
            _TipoNotificacionService = TipoNotificacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoNotificacionDTO>>> Get()
        {
            return Ok(await _TipoNotificacionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoNotificacionDTO>> Get(Guid id)
        {
            var result = await _TipoNotificacionService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TipoNotificacionDTO>> Post(TipoNotificacionDTO dto)
        {
            var created = await _TipoNotificacionService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdTipoNotificacion }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TipoNotificacionDTO dto)
        {
            var updated = await _TipoNotificacionService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _TipoNotificacionService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
