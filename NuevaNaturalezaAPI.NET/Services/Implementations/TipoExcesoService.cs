using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class TipoExcesoService(NuevaNatuContext context, IMapper mapper) : ITipoExcesoService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TipoExcesoDTO>> GetAllAsync()
        {
            var list = await _context.TipoExceso.ToListAsync();
            return _mapper.Map<List<TipoExcesoDTO>>(list);
        }

        public async Task<TipoExcesoDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.TipoExceso.FindAsync(id);
            return item == null ? null : _mapper.Map<TipoExcesoDTO>(item);
        }

        public async Task<TipoExcesoDTO?> CreateAsync(TipoExcesoDTO dto)
        {
            var entity = _mapper.Map<TipoExceso>(dto);
            _context.TipoExceso.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TipoExcesoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, TipoExcesoDTO dto)
        {
            if (id != dto.IdTipoExceso) return false;

            var entity = _mapper.Map<TipoExceso>(dto);
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
            var item = await _context.TipoExceso.FindAsync(id);
            if (item == null) return false;

            _context.TipoExceso.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
