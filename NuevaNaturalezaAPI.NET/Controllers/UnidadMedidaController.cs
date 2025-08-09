using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IUnidadMedidaService _UnidadMedidaService;

        public UnidadMedidaController(IUnidadMedidaService UnidadMedidaService)
        {
            _UnidadMedidaService = UnidadMedidaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedidumDTO>>> Get()
        {
            return Ok(await _UnidadMedidaService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedidumDTO>> Get(Guid id)
        {
            var result = await _UnidadMedidaService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UnidadMedidumDTO>> Post(UnidadMedidumDTO dto)
        {
            var created = await _UnidadMedidaService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdUnidadMedida }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UnidadMedidumDTO dto)
        {
            var updated = await _UnidadMedidaService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _UnidadMedidaService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

