using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AccionActService(NuevaNatuContext context, IMapper mapper) : IAccionActService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<AccionActDTO>> GetAllAsync()
        {
            var list = await _context.AccionAct.ToListAsync();
            return _mapper.Map<List<AccionActDTO>>(list);
        }

        public async Task<AccionActDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.AccionAct.FindAsync(id);
            return item == null ? null : _mapper.Map<AccionActDTO>(item);
        }

        public async Task<AccionActDTO?> CreateAsync(AccionActDTO dto)
        {
            var entity = _mapper.Map<AccionAct>(dto);
            _context.AccionAct.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccionActDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, AccionActDTO dto)
        {
            if (id != dto.IdAccionAct) return false;

            var entity = _mapper.Map<AccionAct>(dto);
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
            var item = await _context.AccionAct.FindAsync(id);
            if (item == null) return false;

            _context.AccionAct.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
