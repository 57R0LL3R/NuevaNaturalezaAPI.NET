using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcasController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> Get()
        {
            return Ok(await _marcaService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaDTO>> Get(Guid id)
        {
            var result = await _marcaService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDTO>> Post(MarcaDTO dto)
        {
            var created = await _marcaService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdMarca }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, MarcaDTO dto)
        {
            var updated = await _marcaService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _marcaService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
