using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosificadorController : ControllerBase
    {
        private readonly IDosificadorService _service;

        public DosificadorController(IDosificadorService service)
        {
            _service = service;
        }

        // GET: api/Dosificador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DosificadorDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Dosificador/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DosificadorDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Dosificador
        [HttpPost]
        public async Task<ActionResult<DosificadorDTO>> Post(DosificadorDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created!.IdDosificador }, created);
        }

        // PUT: api/Dosificador/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, DosificadorDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        // DELETE: api/Dosificador/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
