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
    public class ImpactoesController : ControllerBase
    {
        private readonly IImpactoService _impactoService;

        public ImpactoesController(IImpactoService impactoService)
        {
            _impactoService = impactoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImpactoDTO>>> Get()
        {
            return Ok(await _impactoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImpactoDTO>> Get(Guid id)
        {
            var result = await _impactoService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ImpactoDTO>> Post(ImpactoDTO dto)
        {
            var created = await _impactoService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdImpacto }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ImpactoDTO dto)
        {
            var updated = await _impactoService.UpdateAsync(id, dto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _impactoService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
