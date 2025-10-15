using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TituloController : ControllerBase
    {
        private readonly ITituloService _TituloService;

        public TituloController(ITituloService TituloService)
        {
            _TituloService = TituloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TituloDTO>>> Get()
        {
            return Ok(await _TituloService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TituloDTO>> Get(Guid id)
        {
            var result = await _TituloService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TituloDTO>> Post(TituloDTO dto)
        {
            var created = await _TituloService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdTitulo }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TituloDTO dto)
        {
            var updated = await _TituloService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _TituloService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

