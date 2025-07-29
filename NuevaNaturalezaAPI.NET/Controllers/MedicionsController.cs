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
    public class MedicionsController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public MedicionsController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/Medicions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicion>>> GetMedicions()
        {
            return await _context.Medicions.ToListAsync();
        }

        // GET: api/Medicions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicion>> GetMedicion(Guid id)
        {
            var medicion = await _context.Medicions.FindAsync(id);

            if (medicion == null)
            {
                return NotFound();
            }

            return medicion;
        }

        // PUT: api/Medicions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicion(Guid id, Medicion medicion)
        {
            if (id != medicion.IdMedicion)
            {
                return BadRequest();
            }

            _context.Entry(medicion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicionExists(id))
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

        // POST: api/Medicions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicion>> PostMedicion(Medicion medicion)
        {
            _context.Medicions.Add(medicion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedicionExists(medicion.IdMedicion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMedicion", new { id = medicion.IdMedicion }, medicion);
        }

        // DELETE: api/Medicions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicion(Guid id)
        {
            var medicion = await _context.Medicions.FindAsync(id);
            if (medicion == null)
            {
                return NotFound();
            }

            _context.Medicions.Remove(medicion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicionExists(Guid id)
        {
            return _context.Medicions.Any(e => e.IdMedicion == id);
        }
    }
}
