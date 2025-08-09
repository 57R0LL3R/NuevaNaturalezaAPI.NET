using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
    
        private readonly IRolService _rolService;

        public RolController(IRolService RolService)
        {
            _rolService = RolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> Get()
        {
            return Ok(await _rolService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> Get(Guid id)
        {
            var result = await _rolService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RolDTO>> Post(RolDTO dto)
        {
            var created = await _rolService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdRol }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, RolDTO dto)
        {
            var updated = await _rolService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _rolService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

    }

}
