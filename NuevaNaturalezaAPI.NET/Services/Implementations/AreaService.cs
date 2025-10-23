using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AreaService(NuevaNatuContext context, IMapper mapper) : IAreaService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<AreaDTO>> GetAllAsync()
        {
            var list = await _context.Area.ToListAsync();
            return _mapper.Map<List<AreaDTO>>(list);
        }

        public async Task<AreaDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Area.FindAsync(id);
            return item == null ? null : _mapper.Map<AreaDTO>(item);
        }

        public async Task<AreaDTO?> CreateAsync(AreaDTO dto)
        {
            var entity = _mapper.Map<Area>(dto);
            _context.Area.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AreaDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, AreaDTO dto)
        {
            if (id != dto.IdArea) return false;

            var entity = _mapper.Map<Area>(dto);
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
            var item = await _context.Area.FindAsync(id);
            if (item == null) return false;

            _context.Area.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
