using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class TipoDispositivoService : ITipoDispositivoService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public TipoDispositivoService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoDispositivoDTO>> GetAllAsync()
        {
            var entities = await _context.TipoDispositivos.ToListAsync();
            return _mapper.Map<IEnumerable<TipoDispositivoDTO>>(entities);
        }

        public async Task<TipoDispositivoDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _context.TipoDispositivos.FindAsync(id);
            return entity == null ? null : _mapper.Map<TipoDispositivoDTO>(entity);
        }

        public async Task<TipoDispositivoDTO> CreateAsync(TipoDispositivoDTO dto)
        {
            var entity = _mapper.Map<TipoDispositivo>(dto);
            _context.TipoDispositivos.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TipoDispositivoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, TipoDispositivoDTO dto)
        {
            if (id != dto.IdTipoDispositivo) return false;

            var entity = _mapper.Map<TipoDispositivo>(dto);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return _context.TipoDispositivos.Any(e => e.IdTipoDispositivo == id);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.TipoDispositivos.FindAsync(id);
            if (entity == null) return false;

            _context.TipoDispositivos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
