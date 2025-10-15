using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Authorize(Roles = "Administrador,Operario")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActuadorController(IActuadorService service) : ControllerBase
    {
        private readonly IActuadorService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActuadorDTO>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActuadorDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ActuadorDTO>> Post(ActuadorDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created!.IdActuador }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActuadorDTO dto)
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
