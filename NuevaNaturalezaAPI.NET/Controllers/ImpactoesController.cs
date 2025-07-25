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
    public class ImpactoesController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public ImpactoesController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/Impactoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Impacto>>> GetImpactos()
        {
            return await _context.Impactos.ToListAsync();
        }

        // GET: api/Impactoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Impacto>> GetImpacto(Guid id)
        {
            var impacto = await _context.Impactos.FindAsync(id);

            if (impacto == null)
            {
                return NotFound();
            }

            return impacto;
        }

        // PUT: api/Impactoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImpacto(Guid id, Impacto impacto)
        {
            if (id != impacto.IdImpacto)
            {
                return BadRequest();
            }

            _context.Entry(impacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImpactoExists(id))
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

        // POST: api/Impactoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Impacto>> PostImpacto(Impacto impacto)
        {
            _context.Impactos.Add(impacto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImpactoExists(impacto.IdImpacto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImpacto", new { id = impacto.IdImpacto }, impacto);
        }

        // DELETE: api/Impactoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpacto(Guid id)
        {
            var impacto = await _context.Impactos.FindAsync(id);
            if (impacto == null)
            {
                return NotFound();
            }

            _context.Impactos.Remove(impacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImpactoExists(Guid id)
        {
            return _context.Impactos.Any(e => e.IdImpacto == id);
        }
    }
}
