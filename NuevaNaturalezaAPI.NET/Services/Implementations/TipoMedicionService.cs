using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class TipoMedicionService(NuevaNatuContext context, IMapper mapper) : ITipoMedicionService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TipoMedicionDTO>> GetAllAsync()
        {
            var lista = await _context.TipoMedicions.ToListAsync();
            return _mapper.Map<List<TipoMedicionDTO>>(lista);
        }

        public async Task<TipoMedicionDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.TipoMedicions.FindAsync(id);
            return item == null ? null : _mapper.Map<TipoMedicionDTO>(item);
        }

        public async Task<TipoMedicionDTO?> CreateAsync(TipoMedicionDTO dto)
        {
            var entity = _mapper.Map<TipoMedicion>(dto);
            _context.TipoMedicions.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<TipoMedicionDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, TipoMedicionDTO dto)
        {
            if (id != dto.IdTipoMedicion)
                return false;

            var entity = _mapper.Map<TipoMedicion>(dto);
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
            var entity = await _context.TipoMedicions.FindAsync(id);
            if (entity == null) return false;

            _context.TipoMedicions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
