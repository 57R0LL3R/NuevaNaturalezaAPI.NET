using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class DispositivoService(NuevaNatuContext context, IMapper mapper, ISensorService sensorService,IActuadorService actuadorService) : IDispositivoService
    {
        private readonly IActuadorService _actuadorService= actuadorService;
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ISensorService _sensorService = sensorService;

        public async Task<IEnumerable<DispositivoDTO>> GetAllAsync()
        {
            var sensors = await _sensorService.GetAllAsync();
            var js = new JsonObject{
                ["lol"]="Lol"
            };
            Console.Write(js["lol"]);
            List<Guid> idsDispos = new List<Guid>();
            List<List<SensorDTO>> sensorsbydispo = new List<List<SensorDTO>>();
            var isNew = true;
            foreach (var sensor in sensors) {

                isNew = true;
                foreach (var iddispo in idsDispos)
                {
                    if(sensor.IdDispositivo == iddispo)
                    {
                        isNew = false;
                    }
                }
                if (isNew) {
                    idsDispos.Add(sensor.IdDispositivo);
                    sensorsbydispo.Add([sensor]);
                }
                else
                {
                    int index = idsDispos.IndexOf(sensor.IdDispositivo);
                    sensorsbydispo[index].Add(sensor);
                }
            }
            var list = await _context.Dispositivos.Include(x=>x.IdTipoDispositivoNavigation).Include(x=>x.Actuadores).Include(x => x.Sensors).ToListAsync();
            
            var list1 = _mapper.Map<List<DispositivoDTO>>(list);
            for (var i = 0;i<idsDispos.LongCount();i++  ) 
            {
                var dis = list1.FirstOrDefault(x => x.IdDispositivo == idsDispos[i]);
                if(dis != null)
                {
                    dis.Sensors = sensorsbydispo[i];
                }

                
            }
            return list1;
        }

        public async Task<DispositivoDTO?> GetByIdAsync(Guid id)
        {

            var sensors = await _sensorService.GetAllAsync();
            var item = await _context.Dispositivos.Include(x => x.IdTipoDispositivoNavigation).Include(x => x.Actuadores).Include(x => x.Sensors).FirstOrDefaultAsync(x=>x.IdDispositivo==id);
            if (item is null) return null;
            var itemf = _mapper.Map<DispositivoDTO>(item);
            itemf.Sensors = sensors.Where(x => x.IdDispositivo == itemf.IdDispositivo).ToList();
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
           
            var sensors = await _context.Sensors
                .Include(x => x.IdTipoMUnidadMNavigation)
                .Include(x => x.Medicions)
                .Include(x => x.PuntoOptimos)
                .ToListAsync();
            var item = await _context.Dispositivos.Include(x => x.IdTipoDispositivoNavigation).Include(x => x.Actuadores).Include(x => x.Sensors).FirstOrDefaultAsync(x => x.IdDispositivo == id);
            if (item is null) return false;
            item.Sensors = sensors.Where(x => x.IdDispositivo == item.IdDispositivo).ToList();
            if (id != dto.IdDispositivo||item is null) return false;
            if (id != dto.IdDispositivo ) return false;
            List<string> iddispositivo= new();
            List<SensorDTO> sensorsToDelete= _mapper.Map<List<SensorDTO>>(item.Sensors.ToList());
            if (dto.Sensors != null)
            {
                foreach (var nsen in dto.Sensors)
                {
                    var IsNew = true;
                    foreach(var sen in sensorsToDelete)
                    {
                        if (nsen.IdSensor == sen.IdSensor)
                        {
                            IsNew = false;
                            sensorsToDelete.Remove(sen);
                        }
                    }
                    if (IsNew)
                    {
                        nsen.IdDispositivo = id;
                       await _sensorService.CreateAsync(nsen);
                    }
                    else
                    {
                        await _sensorService.UpdateAsync(nsen.IdSensor,nsen);
                    }
                }
                foreach (var sensor in sensorsToDelete)
                {

                    await _sensorService.DeleteAsync(sensor.IdSensor);

                }
            }
            if (dto.Actuadores != null)
            {

                List<ActuadorDTO> actuadorsToDelete = _mapper.Map<List<ActuadorDTO>>(item.Actuadores.ToList());
                foreach (var nsen in dto.Actuadores)
                {
                    var IsNew = true;
                    foreach (var sen in actuadorsToDelete)
                    {
                        if (nsen.IdActuador == sen.IdActuador)
                        {
                            IsNew = false;
                            actuadorsToDelete.Remove(sen);
                        }
                    }
                    if (IsNew)
                    {
                        nsen.IdDispositivo = id;
                        await _actuadorService.CreateAsync(nsen);
                    }
                    else
                    {
                        await _actuadorService.UpdateAsync(nsen.IdActuador, nsen);
                    }
                }
                foreach (var sensor in actuadorsToDelete)
                {

                    await _actuadorService.DeleteAsync(sensor.IdActuador);

                }

            }
            
            item.Descripcion = dto.Descripcion;
            item.Sn = dto.Sn;
            item.Sensors = new List<Sensor>();
            item.Actuadores = new List<Actuador>();
            item.IdMarca = dto.IdMarca;
            item.IdMarcaNavigation = _mapper.Map<Marca>(dto.IdMarcaNavigation);
            item.Nombre = dto.Nombre;
            item.Image = dto.Image;
            item.IdEstadoDispositivo = dto.IdEstadoDispositivo;
            item.IdTipoDispositivoNavigation = _mapper.Map<TipoDispositivo>(dto.IdTipoDispositivoNavigation);
            item.IdSistema = dto.IdSistema;
            return await Update(item);
            
        }
        public async Task<bool> Update(Dispositivo disF)
        {
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
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            var dispo = _mapper.Map<Dispositivo>(entity);
            _context.Dispositivos.Remove(dispo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
