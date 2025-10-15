using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoExcesoController(ITipoExcesoService service) : ControllerBase
    {
        private readonly ITipoExcesoService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoExcesoDTO>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoExcesoDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TipoExcesoDTO>> Post(TipoExcesoDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created!.IdTipoExceso }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TipoExcesoDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
