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
    public class SistemasController : ControllerBase
    {
        private readonly ISistemaService _service;

        public SistemasController(ISistemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SistemaDTO>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SistemaDTO>> Get(Guid id)
        {
            var sistema = await _service.GetByIdAsync(id);
            return sistema == null ? NotFound() : Ok(sistema);
        }

        [HttpPost]
        public async Task<ActionResult<SistemaDTO>> Post(SistemaDTO sistemaDto)
        {
            var created = await _service.CreateAsync(sistemaDto);
            return CreatedAtAction(nameof(Get), new { id = created.IdSistema }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, SistemaDTO sistemaDto)
        {
            var updated = await _service.UpdateAsync(id, sistemaDto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
