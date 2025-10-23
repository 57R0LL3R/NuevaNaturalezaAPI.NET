using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController(IAreaService service) : ControllerBase
    {
        private readonly IAreaService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDTO>> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AreaDTO>> Post(AreaDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created!.IdArea }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AreaDTO dto)
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
