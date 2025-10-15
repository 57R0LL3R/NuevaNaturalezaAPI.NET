using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Authorize(Roles = "Administrador,Operario")]
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivoesController : ControllerBase
    {
        private readonly IDispositivoService _service;

        public DispositivoesController(IDispositivoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DispositivoDTO>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DispositivoDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DispositivoDTO>> Post(DispositivoDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created!.IdDispositivo }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, DispositivoDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? Ok(dto) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
