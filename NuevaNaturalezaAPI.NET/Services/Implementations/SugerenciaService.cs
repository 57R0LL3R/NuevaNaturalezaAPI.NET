using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class SugerenciaService(NuevaNatuContext context, IMapper mapper) : ISugerenciaService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<SugerenciaDTO>> GetAllAsync()
        {
            var list = await _context.Sugerencias
                .OrderByDescending(x => x.Fecha).ToListAsync();
            return _mapper.Map<List<SugerenciaDTO>>(list);
        }

        public async Task<SugerenciaDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Sugerencias.FindAsync(id);
            return item == null ? null : _mapper.Map<SugerenciaDTO>(item);
        }

        public async Task<SugerenciaDTO?> CreateAsync(SugerenciaDTO dto)
        {
            var entity = _mapper.Map<Sugerencia>(dto);
            _context.Sugerencias.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SugerenciaDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, SugerenciaDTO dto)
        {
            if (id != dto.IdSugerencia) return false;

            var entity = _mapper.Map<Sugerencia>(dto);
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
            var item = await _context.Sugerencias.FindAsync(id);
            if (item == null) return false;

            _context.Sugerencias.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
