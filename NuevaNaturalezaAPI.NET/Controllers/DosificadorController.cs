using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Authorize]
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
        
        [Authorize(Roles = "Administrador,Operario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DosificadorDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Dosificador/{id}
        [Authorize(Roles = "Administrador,Operario")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DosificadorDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Dosificador
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult<DosificadorDTO>> Post(DosificadorDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created!.IdDosificador }, created);
        }

        // PUT: api/Dosificador/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(Guid id, DosificadorDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        // DELETE: api/Dosificador/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
