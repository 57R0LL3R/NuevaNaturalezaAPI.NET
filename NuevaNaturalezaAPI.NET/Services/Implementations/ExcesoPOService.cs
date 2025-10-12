using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ExcesoPOService(NuevaNatuContext context, IMapper mapper) : IExcesoPOService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExcesoPuntoOptimoDTO>> GetAllAsync()
        {
            var list = await _context.ExcesoPuntoOptimo.Include(x=>x.IdAccionActNavigation).Include(x => x.IdTipoExcesoNavigation).ToListAsync();
            return _mapper.Map<List<ExcesoPuntoOptimoDTO>>(list);
        }

        public async Task<ExcesoPuntoOptimoDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.ExcesoPuntoOptimo.Include(x => x.IdAccionActNavigation).Include(x => x.IdTipoExcesoNavigation).FirstOrDefaultAsync(x=>x.IdExcesoPuntoOptimo==id);
            return item == null ? null : _mapper.Map<ExcesoPuntoOptimoDTO>(item);
        }

        public async Task<ExcesoPuntoOptimoDTO?> CreateAsync(ExcesoPuntoOptimoDTO dto)
        {
            var entity = _mapper.Map<ExcesoPuntoOptimo>(dto);
            _context.ExcesoPuntoOptimo.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExcesoPuntoOptimoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ExcesoPuntoOptimoDTO dto)
        {
            if (id != dto.IdExcesoPuntoOptimo) return false;

            var entity = _mapper.Map<ExcesoPuntoOptimo>(dto);
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
            var item = await _context.ExcesoPuntoOptimo.FindAsync(id);
            if (item == null) return false;

            _context.ExcesoPuntoOptimo.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}