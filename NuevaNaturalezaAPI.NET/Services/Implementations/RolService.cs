using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class RolService(NuevaNatuContext context, IMapper mapper) : IRolService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<RolDTO>> GetAllAsync()
        {
            var lista = await _context.Rols.ToListAsync();
            return _mapper.Map<List<RolDTO>>(lista);
        }

        public async Task<RolDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Rols.FindAsync(id);
            return item == null ? null : _mapper.Map<RolDTO>(item);
        }

        public async Task<RolDTO?> CreateAsync(RolDTO dto)
        {
            var entity = _mapper.Map<Rol>(dto);
            _context.Rols.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<RolDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, RolDTO dto)
        {
            if (id != dto.IdRol)
                return false;

            var entity = _mapper.Map<Rol>(dto);
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
            var entity = await _context.Rols.FindAsync(id);
            if (entity == null) return false;

            _context.Rols.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
