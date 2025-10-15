using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class DosificadorService(NuevaNatuContext context, IMapper mapper) : IDosificadorService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<DosificadorDTO>> GetAllAsync()
        {
            var list = await _context.Dosificadores
                .Include(d => d.Programaciones)
                .ToListAsync();
            return _mapper.Map<List<DosificadorDTO>>(list);
        }

        public async Task<DosificadorDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Dosificadores
                .Include(d => d.Programaciones)
                .FirstOrDefaultAsync(x => x.IdDosificador == id);
            return item == null ? null : _mapper.Map<DosificadorDTO>(item);
        }

        public async Task<DosificadorDTO?> CreateAsync(DosificadorDTO dto)
        {
            var entity = _mapper.Map<Dosificador>(dto);
            _context.Dosificadores.Add(entity);
            try { 
            await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _mapper.Map<DosificadorDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, DosificadorDTO dto)
        {
            if (id != dto.IdDosificador) return false;
            var entity = await _context.Dosificadores.FindAsync(id);
            if (entity == null) return false;

            entity.IdDispositivo = dto.IdDispositivo;
            entity.LetraActivacion = dto.LetraActivacion;
            entity.Descripcion = dto.Descripcion;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.Dosificadores.FindAsync(id);
            if (item == null) return false;
            _context.Dosificadores.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
