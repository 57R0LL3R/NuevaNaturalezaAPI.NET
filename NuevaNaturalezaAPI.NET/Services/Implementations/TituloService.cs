using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class TituloService(NuevaNatuContext context, IMapper mapper) : ITituloService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TituloDTO>> GetAllAsync()
        {
            var lista = await _context.Titulos.ToListAsync();
            return _mapper.Map<List<TituloDTO>>(lista);
        }

        public async Task<TituloDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Titulos.FindAsync(id);
            return item == null ? null : _mapper.Map<TituloDTO>(item);
        }

        public async Task<TituloDTO?> CreateAsync(TituloDTO dto)
        {
            var entity = _mapper.Map<Titulo>(dto);
            _context.Titulos.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<TituloDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, TituloDTO dto)
        {
            if (id != dto.IdTitulo)
                return false;

            var entity = _mapper.Map<Titulo>(dto);
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
            var entity = await _context.Titulos.FindAsync(id);
            if (entity == null) return false;

            _context.Titulos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
