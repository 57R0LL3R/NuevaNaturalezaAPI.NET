using Microsoft.AspNetCore.Mvc;
using NuevaNaturalezaAPI.NET.DTOs;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly IChecklistService ChecklistService;

        public ChecklistController(IChecklistService context)
        {
            ChecklistService = context;
        }

        // GET: api/Checklist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChecklistDTO>>> GetChecklists()
        {
            var lista = await ChecklistService.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/Checklist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChecklistDTO>> GetChecklist(Guid id)
        {
            var checklist = await ChecklistService.GetByIdAsync(id);
            return checklist == null ? NotFound() : Ok(checklist);
        }

        // POST: api/Checklist
        [HttpPost]
        public async Task<ActionResult<ChecklistDTO>> PostChecklist(ChecklistDTO dto)
        {
            var created = await ChecklistService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetChecklist), new { id = created.IdChecklist }, created);
        }

        // PUT: api/Checklist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChecklist(Guid id, ChecklistDTO dto)
        {
            var updated = await ChecklistService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        // DELETE: api/Checklist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChecklist(Guid id)
        {
            var deleted = await ChecklistService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
