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
    public class DispositivoesController : ControllerBase
    {
        private readonly NuevaNatuContext _context;

        public DispositivoesController(NuevaNatuContext context)
        {
            _context = context;
        }

        // GET: api/Dispositivoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispositivo>>> GetDispositivos()
        {
            return await _context.Dispositivos.ToListAsync();
        }

        // GET: api/Dispositivoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dispositivo>> GetDispositivo(Guid id)
        {
            var dispositivo = await _context.Dispositivos.FindAsync(id);

            if (dispositivo == null)
            {
                return NotFound();
            }

            return dispositivo;
        }

        // PUT: api/Dispositivoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivo(Guid id, Dispositivo dispositivo)
        {
            if (id != dispositivo.IdDispositivo)
            {
                return BadRequest();
            }

            _context.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivoExists(id))
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

        // POST: api/Dispositivoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dispositivo>> PostDispositivo(Dispositivo dispositivo)
        {
            _context.Dispositivos.Add(dispositivo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DispositivoExists(dispositivo.IdDispositivo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDispositivo", new { id = dispositivo.IdDispositivo }, dispositivo);
        }

        // DELETE: api/Dispositivoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivo(Guid id)
        {
            var dispositivo = await _context.Dispositivos.FindAsync(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            _context.Dispositivos.Remove(dispositivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispositivoExists(Guid id)
        {
            return _context.Dispositivos.Any(e => e.IdDispositivo == id);
        }
    }
}
