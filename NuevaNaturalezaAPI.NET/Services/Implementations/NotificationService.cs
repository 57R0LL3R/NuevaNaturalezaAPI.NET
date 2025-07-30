using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
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

        public NotificacionService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificacionDTO>> GetAllAsync()
        {
            var entities = await _context.Notificacions.ToListAsync();
            return _mapper.Map<List<NotificacionDTO>>(entities);
        }

        public async Task<NotificacionDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Notificacions.FindAsync(id);
            return entity == null ? null : _mapper.Map<NotificacionDTO>(entity);
        }

        public async Task<NotificacionDTO?> CreateAsync(NotificacionDTO dto)
        {
            var entity = _mapper.Map<Notificacion>(dto);
            _context.Notificacions.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
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
