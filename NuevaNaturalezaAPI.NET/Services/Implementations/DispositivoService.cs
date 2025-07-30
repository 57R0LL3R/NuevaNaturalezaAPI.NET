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
    public class DispositivoService : IDispositivoService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public DispositivoService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DispositivoDTO>> GetAllAsync()
        {
            var list = await _context.Dispositivos.ToListAsync();
            return _mapper.Map<List<DispositivoDTO>>(list);
        }

        public async Task<DispositivoDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Dispositivos.FindAsync(id);
            return item == null ? null : _mapper.Map<DispositivoDTO>(item);
        }

        public async Task<DispositivoDTO?> CreateAsync(DispositivoDTO dto)
        {
            var entity = _mapper.Map<Dispositivo>(dto);
            _context.Dispositivos.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DispositivoDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, DispositivoDTO dto)
        {
            if (id != dto.IdDispositivo) return false;

            var entity = _mapper.Map<Dispositivo>(dto);
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
            var entity = await _context.Dispositivos.FindAsync(id);
            if (entity == null) return false;

            _context.Dispositivos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
