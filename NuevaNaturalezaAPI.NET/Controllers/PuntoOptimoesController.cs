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
    public class PuntoOptimoesController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public PuntoOptimoesController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/PuntoOptimoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntoOptimo>>> GetPuntoOptimos()
        {
            return await _context.PuntoOptimos.ToListAsync();
        }

        // GET: api/PuntoOptimoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PuntoOptimo>> GetPuntoOptimo(Guid id)
        {
            var puntoOptimo = await _context.PuntoOptimos.FindAsync(id);

            if (puntoOptimo == null)
            {
                return NotFound();
            }

            return puntoOptimo;
        }

        // PUT: api/PuntoOptimoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuntoOptimo(Guid id, PuntoOptimo puntoOptimo)
        {
            if (id != puntoOptimo.IdPuntoOptimo)
            {
                return BadRequest();
            }

            _context.Entry(puntoOptimo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntoOptimoExists(id))
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

        // POST: api/PuntoOptimoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PuntoOptimo>> PostPuntoOptimo(PuntoOptimo puntoOptimo)
        {
            _context.PuntoOptimos.Add(puntoOptimo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PuntoOptimoExists(puntoOptimo.IdPuntoOptimo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPuntoOptimo", new { id = puntoOptimo.IdPuntoOptimo }, puntoOptimo);
        }

        // DELETE: api/PuntoOptimoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuntoOptimo(Guid id)
        {
            var puntoOptimo = await _context.PuntoOptimos.FindAsync(id);
            if (puntoOptimo == null)
            {
                return NotFound();
            }

            _context.PuntoOptimos.Remove(puntoOptimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuntoOptimoExists(Guid id)
        {
            return _context.PuntoOptimos.Any(e => e.IdPuntoOptimo == id);
        }
    }
}
