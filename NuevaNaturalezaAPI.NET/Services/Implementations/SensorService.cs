using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System.Linq;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class SensorService(NuevaNatuContext context, IMapper mapper,ITipoMUnidadMService TipoMUnidadMService,IPuntoOptimoService puntoOptimoService) : ISensorService
    {
        private readonly IPuntoOptimoService _puntoOptimoService = puntoOptimoService;
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ITipoMUnidadMService _tipoMUnidadMService = TipoMUnidadMService;
        public async Task<IEnumerable<SensorDTO>> GetAllAsync()
        {
            var tipounidadm = await _tipoMUnidadMService.GetAllAsync();
            var lista = await _context.Sensors
                .Include(x=>x.IdTipoMUnidadMNavigation)
                .Include(x => x.Medicions)
                .Include(x => x.PuntoOptimos)
                .ToListAsync();
            var list1 = _mapper.Map<List<SensorDTO>>(lista);
            foreach (var item in list1)
            {
                if (item.IdTipoMUnidadMNavigation is not null) {
                    var tmum = tipounidadm.FirstOrDefault(x => item.IdTipoMUnidadM == x.IdTipoMUnidadM);
                    item.IdTipoMUnidadMNavigation = tmum;
                }
            }
            
            return list1 ;
        }

        public async Task<SensorDTO?> GetByIdAsync(Guid id)
        {
            var tipounidadm = await _tipoMUnidadMService.GetAllAsync();
            var item = await _context.Sensors
                .Include(x => x.IdTipoMUnidadMNavigation)
                .Include(x => x.Medicions)
                .Include(x => x.PuntoOptimos)
                .FirstAsync(X => X.IdSensor == id);

            if (item.IdTipoMUnidadMNavigation is not null)
            {
                var tmum = tipounidadm.FirstOrDefault(x => item.IdTipoMUnidadM == x.IdTipoMUnidadM);
                item.IdTipoMUnidadMNavigation = _mapper.Map < TipoMUnidadM > (tmum);
            }
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
            var oldsen = await GetByIdAsync(id);
            if (id != dto.IdSensor||oldsen is null)
                return false;



            if (oldsen.PuntoOptimos != dto.PuntoOptimos)
            {
                if(dto.PuntoOptimos != null)
                {

                    await _puntoOptimoService.UpdateAsync(dto.PuntoOptimos.Last().IdPuntoOptimo,dto.PuntoOptimos.Last());

                }
                
            }

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
            var entity = await _context.Sensors.Include(X=>X.PuntoOptimos).FirstOrDefaultAsync(x=>x.IdSensor==id);
            if (entity == null) return false;

            _context.Sensors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
