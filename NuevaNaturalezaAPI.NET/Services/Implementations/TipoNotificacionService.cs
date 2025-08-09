using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class TipoNotificacionService(NuevaNatuContext context, IMapper mapper) : ITipoNotificacionService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TipoNotificacionDTO>> GetAllAsync()
        {
            var lista = await _context.TipoNotificacions.ToListAsync();
            return _mapper.Map<List<TipoNotificacionDTO>>(lista);
        }

        public async Task<TipoNotificacionDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.TipoNotificacions.FindAsync(id);
            return item == null ? null : _mapper.Map<TipoNotificacionDTO>(item);
        }

        public async Task<TipoNotificacionDTO?> CreateAsync(TipoNotificacionDTO dto)
        {
            var entity = _mapper.Map<TipoNotificacion>(dto);
            _context.TipoNotificacions.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<TipoNotificacionDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, TipoNotificacionDTO dto)
        {
            if (id != dto.IdTipoNotificacion)
                return false;

            var entity = _mapper.Map<TipoNotificacion>(dto);
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
            var entity = await _context.TipoNotificacions.FindAsync(id);
            if (entity == null) return false;

            _context.TipoNotificacions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
