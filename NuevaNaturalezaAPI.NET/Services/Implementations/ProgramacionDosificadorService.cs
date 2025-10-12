using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ProgramacionDosificadorService(NuevaNatuContext context, IMapper mapper) : IProgramacionDosificadorService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProgramacionDosificadorDTO>> GetAllAsync()
        {
            var list = await _context.ProgramacionDosificadores
                .Include(x => x.Dosificador)
                    .ThenInclude(d => d.IdDispositivoNavigation)
                .ToListAsync();

            return list.Select(p => new ProgramacionDosificadorDTO
            {
                IdProgramacion = p.IdProgramacion,
                IdDosificador = p.IdDosificador,
                Hora = p.Hora,
                Minuto = p.Minuto,
                TiempoSegundos = p.TiempoSegundos,
                // puedes mostrar la letra del dosificador o el nombre del dispositivo padre
                NombreDosificador = p.Dosificador?.IdDispositivoNavigation?.Nombre ?? p.Dosificador?.LetraActivacion
            });
        }

        public async Task<ProgramacionDosificadorDTO?> GetByIdAsync(Guid id)
        {
            var p = await _context.ProgramacionDosificadores
                .Include(x => x.Dosificador)
                .FirstOrDefaultAsync(x => x.IdProgramacion == id);

            return p == null ? null : new ProgramacionDosificadorDTO
            {
                IdProgramacion = p.IdProgramacion,
                IdDosificador = p.IdDosificador,
                Hora = p.Hora,
                Minuto = p.Minuto,
                TiempoSegundos = p.TiempoSegundos,
                NombreDosificador = p.Dosificador?.IdDispositivoNavigation?.Nombre
                            ?? p.Dosificador?.LetraActivacion
            };
        }

        public async Task<ProgramacionDosificadorDTO?> CreateAsync(ProgramacionDosificadorDTO dto)
        {
            var entity = _mapper.Map<ProgramacionDosificador>(dto);
            _context.ProgramacionDosificadores.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProgramacionDosificadorDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ProgramacionDosificadorDTO dto)
        {
            if (id != dto.IdProgramacion) return false;

            var entity = await _context.ProgramacionDosificadores.FindAsync(id);
            if (entity == null) return false;

            entity.IdDosificador = dto.IdDosificador;
            entity.Hora = dto.Hora;
            entity.Minuto = dto.Minuto;
            entity.TiempoSegundos = dto.TiempoSegundos;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.ProgramacionDosificadores.FindAsync(id);
            if (entity == null) return false;

            _context.ProgramacionDosificadores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
