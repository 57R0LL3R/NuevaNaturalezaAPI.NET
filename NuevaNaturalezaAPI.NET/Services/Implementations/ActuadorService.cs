using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ActuadorService(NuevaNatuContext context, IMapper mapper) : IActuadorService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ActuadorDTO>> GetAllAsync()
        {
            var list = await _context.Actuador.ToListAsync();
            return _mapper.Map<List<ActuadorDTO>>(list);
        }

        public async Task<ActuadorDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Actuador.FindAsync(id);
            return item == null ? null : _mapper.Map<ActuadorDTO>(item);
        }

        public async Task<ActuadorDTO?> CreateAsync(ActuadorDTO dto)
        {
            var entity = _mapper.Map<Actuador>(dto);
            _context.Actuador.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ActuadorDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ActuadorDTO dto)
        {
            if (id != dto.IdActuador) return false;

            var entity = _mapper.Map<Actuador>(dto);
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
            var item = await _context.Actuador.FindAsync(id);
            if (item == null) return false;

            _context.Actuador.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
