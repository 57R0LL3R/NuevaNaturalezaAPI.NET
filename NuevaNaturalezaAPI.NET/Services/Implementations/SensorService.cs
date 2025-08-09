using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class SensorService(NuevaNatuContext context, IMapper mapper) : ISensorService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<SensorDTO>> GetAllAsync()
        {
            var lista = await _context.Sensors.ToListAsync();
            return _mapper.Map<List<SensorDTO>>(lista);
        }

        public async Task<SensorDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Sensors.FindAsync(id);
            return item == null ? null : _mapper.Map<SensorDTO>(item);
        }

        public async Task<SensorDTO?> CreateAsync(SensorDTO dto)
        {
            var entity = _mapper.Map<Sensor>(dto);
            _context.Sensors.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<SensorDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, SensorDTO dto)
        {
            if (id != dto.IdSensor)
                return false;

            var entity = _mapper.Map<Sensor>(dto);
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
            var entity = await _context.Sensors.FindAsync(id);
            if (entity == null) return false;

            _context.Sensors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
