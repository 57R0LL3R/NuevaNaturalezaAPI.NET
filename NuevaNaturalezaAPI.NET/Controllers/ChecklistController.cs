using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.DTOs;
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
    public class ChecklistController : ControllerBase
    {
        private readonly IChecklistService _service;

        public ChecklistController(IChecklistService service)
        {
            _service = service;
        }

        // GET: api/Checklist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChecklistDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Checklist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChecklistDTO>> Get(Guid id)
        {
            var checklist = await _service.GetByIdAsync(id);
            return checklist == null ? NotFound() : Ok(checklist);
        }
        [HttpGet("filtrar")]
        public async Task<ActionResult<ChecklistDTO>>Filtrar([FromQuery] DateTime desde, [FromQuery]DateTime hasta)
        {
            var checklist = await _service.GetAllInterval(desde,hasta);
            return checklist == null ? NotFound() : Ok(checklist);
        }
        
        // POST: api/Checklist
        [HttpPost]
        public async Task<ActionResult<ChecklistDTO>> PostChecklist(ChecklistDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created!.IdChecklist }, created);
        }

        // PUT: api/Checklist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChecklist(Guid id, ChecklistDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        // DELETE: api/Checklist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChecklist(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
