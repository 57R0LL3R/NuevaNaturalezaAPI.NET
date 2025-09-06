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
    public class DispositivoService(NuevaNatuContext context, IMapper mapper, ISensorService sensorService) : IDispositivoService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ISensorService _sensorService = sensorService;

        public async Task<IEnumerable<DispositivoDTO>> GetAllAsync()
        {
            var sensors = await _sensorService.GetAllAsync();
            var list = await _context.Dispositivos.Include(x=>x.IdTipoDispositivoNavigation).ToListAsync();
            var list1 = _mapper.Map<List<DispositivoDTO>>(list);
            return list1;
        }

        public async Task<DispositivoDTO?> GetByIdAsync(Guid id)
        {

            var sensors = await _sensorService.GetAllAsync();
            var item = await _context.Dispositivos.Include(x => x.IdTipoDispositivoNavigation).FirstOrDefaultAsync(x=>x.IdDispositivo==id);
            if (item is null) return null;
            var itemf = _mapper.Map<DispositivoDTO>(item);
            return itemf ;
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
