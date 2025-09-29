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
    public class SugerenciasController : ControllerBase
    {
        private readonly ISugerenciaService SugerenciaService;

        public SugerenciasController(ISugerenciaService context)
        {
            SugerenciaService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sugerencia>>> GetSugerencias()
        {
            return Ok(await SugerenciaService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sugerencia>> Get(Guid id)
        {
            var sistema = await SugerenciaService.GetByIdAsync(id);
            return sistema == null ? NotFound() : Ok(sistema);
        }

        [HttpPost]
        public async Task<ActionResult<Sugerencia>> Post(SugerenciaDTO sugerenciaDto)
        {
            var created = await SugerenciaService.CreateAsync(sugerenciaDto);
            return CreatedAtAction(nameof(Get), new { id = created.IdSugerencia }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, SugerenciaDTO sugerenciaDto)
        {
            var updated = await SugerenciaService.UpdateAsync(id, sugerenciaDto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSugerencia(Guid id)
        {
            var deleted = await SugerenciaService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
