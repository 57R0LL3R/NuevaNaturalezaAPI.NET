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
    public class AuditoriumsController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public AuditoriumsController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/Auditoriums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auditorium>>> GetAuditoria()
        {
            return await _context.Auditoria.ToListAsync();
        }

        // GET: api/Auditoriums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auditorium>> GetAuditorium(Guid id)
        {
            var auditorium = await _context.Auditoria.FindAsync(id);

            if (auditorium == null)
            {
                return NotFound();
            }

            return auditorium;
        }

        // PUT: api/Auditoriums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuditorium(Guid id, Auditorium auditorium)
        {
            if (id != auditorium.IdAuditoria)
            {
                return BadRequest();
            }

            _context.Entry(auditorium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditoriumExists(id))
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

        // POST: api/Auditoriums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Auditorium>> PostAuditorium(Auditorium auditorium)
        {
            _context.Auditoria.Add(auditorium);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuditoriumExists(auditorium.IdAuditoria))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAuditorium", new { id = auditorium.IdAuditoria }, auditorium);
        }

        // DELETE: api/Auditoriums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditorium(Guid id)
        {
            var auditorium = await _context.Auditoria.FindAsync(id);
            if (auditorium == null)
            {
                return NotFound();
            }

            _context.Auditoria.Remove(auditorium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuditoriumExists(Guid id)
        {
            return _context.Auditoria.Any(e => e.IdAuditoria == id);
        }
    }
}
