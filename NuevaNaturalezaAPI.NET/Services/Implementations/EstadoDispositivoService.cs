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
    public class EstadoDispositivoService : IEstadoDispositivoService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public EstadoDispositivoService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstadoDispositivoDTO>> GetAllAsync()
        {
            var lista = await _context.EstadoDispositivos.ToListAsync();
            return _mapper.Map<List<EstadoDispositivoDTO>>(lista);
        }

        public async Task<EstadoDispositivoDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.EstadoDispositivos.FindAsync(id);
            return item == null ? null : _mapper.Map<EstadoDispositivoDTO>(item);
        }

        public async Task<EstadoDispositivoDTO?> CreateAsync(EstadoDispositivoDTO dto)
        {
            var entity = _mapper.Map<EstadoDispositivo>(dto);
            _context.EstadoDispositivos.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EstadoDispositivoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, EstadoDispositivoDTO dto)
        {
            if (id != dto.IdEstadoDispositivo) return false;
            var entity = _mapper.Map<EstadoDispositivo>(dto);
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
            var item = await _context.EstadoDispositivos.FindAsync(id);
            if (item == null) return false;
            _context.EstadoDispositivos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
