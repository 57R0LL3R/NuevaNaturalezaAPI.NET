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
    public class FechaMedicionsController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public FechaMedicionsController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/FechaMedicions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FechaMedicion>>> GetFechaMedicions()
        {
            return await _context.FechaMedicions.ToListAsync();
        }

        // GET: api/FechaMedicions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FechaMedicion>> GetFechaMedicion(Guid id)
        {
            var fechaMedicion = await _context.FechaMedicions.FindAsync(id);

            if (fechaMedicion == null)
            {
                return NotFound();
            }

            return fechaMedicion;
        }

        // PUT: api/FechaMedicions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFechaMedicion(Guid id, FechaMedicion fechaMedicion)
        {
            if (id != fechaMedicion.IdFechaMedicion)
            {
                return BadRequest();
            }

            _context.Entry(fechaMedicion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaMedicionExists(id))
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

        // POST: api/FechaMedicions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FechaMedicion>> PostFechaMedicion(FechaMedicion fechaMedicion)
        {
            _context.FechaMedicions.Add(fechaMedicion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FechaMedicionExists(fechaMedicion.IdFechaMedicion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFechaMedicion", new { id = fechaMedicion.IdFechaMedicion }, fechaMedicion);
        }

        // DELETE: api/FechaMedicions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFechaMedicion(Guid id)
        {
            var fechaMedicion = await _context.FechaMedicions.FindAsync(id);
            if (fechaMedicion == null)
            {
                return NotFound();
            }

            _context.FechaMedicions.Remove(fechaMedicion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FechaMedicionExists(Guid id)
        {
            return _context.FechaMedicions.Any(e => e.IdFechaMedicion == id);
        }
    }
}
