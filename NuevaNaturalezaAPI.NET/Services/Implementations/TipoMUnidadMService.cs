using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class TipoMUnidadMService(NuevaNatuContext context, IMapper mapper) : ITipoMUnidadMService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TipoMUnidadMDTO>> GetAllAsync()
        {
            var lista = await _context.TipoMUnidadMs
                .Include(x => x.IdTipoMedicionNavigation)
                .Include(x=>x.IdUnidadMedidaNavigation)
                .ToListAsync();
            return _mapper.Map<List<TipoMUnidadMDTO>>(lista);
        }

        public async Task<TipoMUnidadMDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.TipoMUnidadMs
                .Include(x => x.IdTipoMedicionNavigation)
                .Include(x => x.IdUnidadMedidaNavigation)
                .FirstOrDefaultAsync(x=>x.IdTipoMUnidadM==id);
            return item == null ? null : _mapper.Map<TipoMUnidadMDTO>(item);
        }

        public async Task<TipoMUnidadMDTO?> CreateAsync(TipoMUnidadMDTO dto)
        {
            var entity = _mapper.Map<TipoMUnidadM>(dto);
            _context.TipoMUnidadMs.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<TipoMUnidadMDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, TipoMUnidadMDTO dto)
        {
            if (id != dto.IdTipoMUnidadM)
                return false;

            var entity = _mapper.Map<TipoMUnidadM>(dto);
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
            var entity = await _context.TipoMUnidadMs.FindAsync(id);
            if (entity == null) return false;

            _context.TipoMUnidadMs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
