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
    public class PuntoOptimoesController : ControllerBase
    {
        private readonly IPuntoOptimoService _service;

        public PuntoOptimoesController(IPuntoOptimoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntoOptimoDTO>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PuntoOptimoDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PuntoOptimoDTO>> Post(PuntoOptimoDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdPuntoOptimo }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PuntoOptimoDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
