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
    public class NotificacionsController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public NotificacionsController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/Notificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> GetNotificacions()
        {
            return await _context.Notificacions.ToListAsync();
        }

        // GET: api/Notificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacion>> GetNotificacion(Guid id)
        {
            var notificacion = await _context.Notificacions.FindAsync(id);

            if (notificacion == null)
            {
                return NotFound();
            }

            return notificacion;
        }

        // PUT: api/Notificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificacion(Guid id, Notificacion notificacion)
        {
            if (id != notificacion.IdNotificacion)
            {
                return BadRequest();
            }

            _context.Entry(notificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacionExists(id))
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

        // POST: api/Notificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notificacion>> PostNotificacion(Notificacion notificacion)
        {
            _context.Notificacions.Add(notificacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotificacionExists(notificacion.IdNotificacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNotificacion", new { id = notificacion.IdNotificacion }, notificacion);
        }

        // DELETE: api/Notificacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(Guid id)
        {
            var notificacion = await _context.Notificacions.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }

            _context.Notificacions.Remove(notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificacionExists(Guid id)
        {
            return _context.Notificacions.Any(e => e.IdNotificacion == id);
        }
    }
}
