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
    public class NotificacionsController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionsController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacionDTO>>> Get()
        {
            var result = await _notificacionService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacionDTO>> Get(Guid id)
        {
            var result = await _notificacionService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<NotificacionDTO>> Post(NotificacionDTO dto)
        {
            var created = await _notificacionService.CreateAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = created.IdNotificacion }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, NotificacionDTO dto)
        {
            var success = await _notificacionService.UpdateAsync(id, dto);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _notificacionService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
