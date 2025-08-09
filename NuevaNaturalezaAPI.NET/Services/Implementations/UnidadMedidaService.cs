using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class UnidadMedidaService(NuevaNatuContext context, IMapper mapper) : IUnidadMedidaService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UnidadMedidumDTO>> GetAllAsync()
        {
            var lista = await _context.UnidadMedida.ToListAsync();
            return _mapper.Map<List<UnidadMedidumDTO>>(lista);
        }

        public async Task<UnidadMedidumDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.UnidadMedida.FindAsync(id);
            return item == null ? null : _mapper.Map<UnidadMedidumDTO>(item);
        }

        public async Task<UnidadMedidumDTO?> CreateAsync(UnidadMedidumDTO dto)
        {
            var entity = _mapper.Map<UnidadMedidum>(dto);
            _context.UnidadMedida.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<UnidadMedidumDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, UnidadMedidumDTO dto)
        {
            if (id != dto.IdUnidadMedida)
                return false;

            var entity = _mapper.Map<UnidadMedidum>(dto);
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
            var entity = await _context.UnidadMedida.FindAsync(id);
            if (entity == null) return false;

            _context.UnidadMedida.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
