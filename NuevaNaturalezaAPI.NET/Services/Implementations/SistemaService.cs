using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class SistemaService : ISistemaService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public SistemaService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SistemaDTO>> GetAllAsync()
        {
            var sistemas = await _context.Sistemas.ToListAsync();
            return _mapper.Map<IEnumerable<SistemaDTO>>(sistemas);
        }

        public async Task<SistemaDTO?> GetByIdAsync(Guid id)
        {
            var sistema = await _context.Sistemas.FindAsync(id);
            return sistema == null ? null : _mapper.Map<SistemaDTO>(sistema);
        }

        public async Task<SistemaDTO> CreateAsync(SistemaDTO sistemaDto)
        {
            var entity = _mapper.Map<Sistema>(sistemaDto);
            _context.Sistemas.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<SistemaDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, SistemaDTO sistemaDto)
        {
            if (id != sistemaDto.IdSistema) return false;

            var entity = _mapper.Map<Sistema>(sistemaDto);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return _context.Sistemas.Any(e => e.IdSistema == id);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema == null) return false;

            _context.Sistemas.Remove(sistema);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
