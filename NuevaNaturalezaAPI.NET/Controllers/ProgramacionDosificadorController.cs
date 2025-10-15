using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Acuaponic.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramacionDosificacionController : ControllerBase
    {
        private readonly IProgramacionDosificadorService _service;

        public ProgramacionDosificacionController(IProgramacionDosificadorService service)
        {
            _service = service;
        }

        // GET: api/ProgramacionDosificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramacionDosificadorDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/ProgramacionDosificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramacionDosificadorDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProgramacionDosificadorDTO>> Post(ProgramacionDosificadorDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(GetAll), new { id = created!.IdProgramacion }, created);
        }

        // PUT: api/ProgramacionDosificacion/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id,ProgramacionDosificadorDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        // DELETE: api/ProgramacionDosificacion/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
