using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class NotificacionService : INotificacionService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        private readonly IHubContext<SignalRHub> _hubContext;

        public NotificacionService(NuevaNatuContext context, IMapper mapper, IHubContext<SignalRHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<NotificacionDTO>> GetAllAsync()
        {
            var entities = await _context.Notificacions
                .Include(x => x.IdTipoNotificacionNavigation)
                .Include(x => x.IdTituloNavigation)
                .ToListAsync();
            return _mapper.Map<List<NotificacionDTO>>(entities);
        }

        public async Task<NotificacionDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Notificacions
                .Include(x=>x.IdTipoNotificacionNavigation)
                .Include(x => x.IdTituloNavigation)
                .FirstOrDefaultAsync(x=>x.IdNotificacion==id);
            return entity == null ? null : _mapper.Map<NotificacionDTO>(entity);
        }

        public async Task<NotificacionDTO?> CreateAsync(NotificacionDTO dto)
        {
            var entity = _mapper.Map<Notificacion>(dto);
            _context.Notificacions.Add(entity);
            try
            {
                //await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", new
                {
                    tipo = "notificacion",
                    payload = 
                        entity
                    
                } );
                return _mapper.Map<NotificacionDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, NotificacionDTO dto)
        {
            if (id != dto.IdNotificacion)
                return false;

            var entity = _mapper.Map<Notificacion>(dto);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Notificacions.FindAsync(id);
            if (entity == null) return false;

            _context.Notificacions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
