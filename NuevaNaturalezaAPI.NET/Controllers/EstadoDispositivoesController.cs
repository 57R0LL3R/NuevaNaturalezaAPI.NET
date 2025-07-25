using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models;

namespace NuevaNaturalezaAPI.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoDispositivoesController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public EstadoDispositivoesController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/EstadoDispositivoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDispositivo>>> GetEstadoDispositivos()
        {
            return await _context.EstadoDispositivos.ToListAsync();
        }

        // GET: api/EstadoDispositivoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDispositivo>> GetEstadoDispositivo(Guid id)
        {
            var estadoDispositivo = await _context.EstadoDispositivos.FindAsync(id);

            if (estadoDispositivo == null)
            {
                return NotFound();
            }

            return estadoDispositivo;
        }

        // PUT: api/EstadoDispositivoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoDispositivo(Guid id, EstadoDispositivo estadoDispositivo)
        {
            if (id != estadoDispositivo.IdEstadoDispositivo)
            {
                return BadRequest();
            }

            _context.Entry(estadoDispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoDispositivoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EstadoDispositivoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoDispositivo>> PostEstadoDispositivo(EstadoDispositivo estadoDispositivo)
        {
            _context.EstadoDispositivos.Add(estadoDispositivo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EstadoDispositivoExists(estadoDispositivo.IdEstadoDispositivo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEstadoDispositivo", new { id = estadoDispositivo.IdEstadoDispositivo }, estadoDispositivo);
        }

        // DELETE: api/EstadoDispositivoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoDispositivo(Guid id)
        {
            var estadoDispositivo = await _context.EstadoDispositivos.FindAsync(id);
            if (estadoDispositivo == null)
            {
                return NotFound();
            }

            _context.EstadoDispositivos.Remove(estadoDispositivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoDispositivoExists(Guid id)
        {
            return _context.EstadoDispositivos.Any(e => e.IdEstadoDispositivo == id);
        }
    }
}
