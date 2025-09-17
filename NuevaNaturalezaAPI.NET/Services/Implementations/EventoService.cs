using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class EventoService : IEventoService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public EventoService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventoDTO>> GetAllAsync()
        {
            var lista = await _context.Eventos
                .Include(x => x.IdDispositivoNavigation)
                .Include(x => x.IdSistemaNavigation)
                .Include(x => x.IdImpactoNavigation)
                .ToListAsync();
            return _mapper.Map<List<EventoDTO>>(lista);
        }

        public async Task<EventoDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Eventos
                .Include(x => x.IdDispositivoNavigation)
                .Include(x => x.IdSistemaNavigation)
                .Include(x => x.IdImpactoNavigation)
                .FirstOrDefaultAsync(x=>x.IdEvento==id);
            return item == null ? null : _mapper.Map<EventoDTO>(item);
        }

        public async Task<EventoDTO?> CreateAsync(EventoDTO dto)
        {
            var entity = _mapper.Map<Evento>(dto);
            _context.Eventos.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, EventoDTO dto)
        {
            if (id != dto.IdEvento) return false;
            var entity = _mapper.Map<Evento>(dto);
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
            var item = await _context.Eventos.FindAsync(id);
            if (item == null) return false;
            _context.Eventos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
