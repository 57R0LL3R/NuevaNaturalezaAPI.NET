using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuGet.DependencyResolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class DispositivoService(NuevaNatuContext context, IMapper mapper, ISensorService sensorService,IActuadorService actuadorService,IExcesoPOService excesoService) : IDispositivoService
    {
        private readonly IActuadorService _actuadorService= actuadorService;
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ISensorService _sensorService = sensorService;
        private readonly IExcesoPOService _excesoService = excesoService;

        public async Task<IEnumerable<DispositivoDTO>> GetAllAsync(bool data = false)
        {

            var now = DateTime.UtcNow;
            var dispositivos = await _context.Dispositivos
                .Include(d => d.IdTipoDispositivoNavigation)
                .Include(d => d.IdMarcaNavigation)
                .Include(d => d.IdSistemaNavigation)
                .Include(d => d.IdEstadoDispositivoNavigation)
                .Include(d => d.Actuadores)
                    .ThenInclude(a => a.IdAccionActNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.PuntoOptimos)
                        .ThenInclude(po => po.ExcesoPuntosOptimos)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                        .ThenInclude(t => t.IdTipoMedicionNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                        .ThenInclude(t => t.IdUnidadMedidaNavigation)
                 .Include(d => d.Sensors)
                    .ThenInclude(s => s.Medicions 
                    .Where(m => m.IdFechaMedicionNavigation.Fecha < now && data)
                        .OrderByDescending(m => m.IdFechaMedicionNavigation.Fecha)
                           .Take(20))
                        .ThenInclude(m => m.IdFechaMedicionNavigation).ToListAsync()
                                    ;
            if (data)
            {
                foreach (var dispositivo in dispositivos)
                {
                    foreach (var sensor in dispositivo.Sensors)
                    {
                        sensor.Medicions = sensor.Medicions
                            .OrderBy(m => m.IdFechaMedicionNavigation.Fecha)
                            .ToList();
                    }
                }
            }



            var list1 = _mapper.Map<List<DispositivoDTO>>(dispositivos);
            return list1;
        }

        public async Task<DispositivoDTO?> GetByIdAsync(Guid id)
        {

            var sensors = await _sensorService.GetAllAsync();
            var item = await _context.Dispositivos
                .Include(x => x.IdTipoDispositivoNavigation)
                .Include(x => x.IdMarcaNavigation)
                .Include(x => x.Actuadores)
                    .ThenInclude(a => a.IdAccionActNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.PuntoOptimos)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.Medicions)
                        .ThenInclude(m => m.IdFechaMedicionNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                        .ThenInclude(t => t.IdTipoMedicionNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                .FirstOrDefaultAsync(x=>x.IdDispositivo==id);
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
            var item = await _context.Dispositivos
                .Include(x => x.IdTipoDispositivoNavigation)
                .Include(x => x.IdMarcaNavigation)
                .Include(x => x.Actuadores)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.PuntoOptimos)
                        .ThenInclude(po => po.ExcesoPuntosOptimos)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.Medicions)
                        .ThenInclude(m => m.IdFechaMedicionNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                        .ThenInclude(t => t.IdTipoMedicionNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                .FirstOrDefaultAsync(x => x.IdDispositivo == id);
            if (item is null || id != dto.IdDispositivo) return false;
            await AddUpdates(item, dto);
            item.Descripcion = dto.Descripcion;
            item.Sn = dto.Sn;
            item.IdMarca = dto.IdMarca;
            item.IdMarcaNavigation = _mapper.Map<Marca>(dto.IdMarcaNavigation);
            item.Nombre = dto.Nombre;
            item.Image = dto.Image;
            item.IdEstadoDispositivo = dto.IdEstadoDispositivo;
            if(_mapper.Map<TipoDispositivo>(dto.IdTipoDispositivoNavigation) != null )
                item.IdTipoDispositivoNavigation = _mapper.Map<TipoDispositivo>(dto.IdTipoDispositivoNavigation);
            if (dto.IdTipoDispositivo != null)
                item.IdTipoDispositivo = dto.IdTipoDispositivo;
            item.IdSistema = dto.IdSistema;
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            

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

            var item = await _context.Dispositivos
                .Include(x => x.IdTipoDispositivoNavigation)
                .Include(x => x.Eventos)
                .Include(x=>x.Auditoria)
                .Include(x => x.Actuadores)
                .Include(x=>x.Sensors)
                    .ThenInclude(x=>x.Medicions)
                .Include(x => x.Sensors)
                    .ThenInclude(x => x.PuntoOptimos)
                .FirstOrDefaultAsync(x => x.IdDispositivo == id);
            if (item is null) return false;
            if (item.Eventos.Count > 0)
            {
                _context.Eventos.RemoveRange(item.Eventos);
                await _context.SaveChangesAsync();
            }
            if (item.Auditoria.Count>0)
            {
                _context.Auditoria.RemoveRange(item.Auditoria);
                await _context.SaveChangesAsync();
            }
            _context.Dispositivos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        private async Task AddUpdates(Dispositivo item, DispositivoDTO dto)
        {
            List<string> iddispositivo = new();
            List<SensorDTO> sensorsListDto = _mapper.Map<List<SensorDTO>>(item.Sensors.ToList());
            List<SensorDTO> sensorsToDelete = [.. sensorsListDto];

            if (dto.Sensors != null)
            {
                foreach (var nsen in dto.Sensors)
                {
                    var IsNew = true;
                    foreach (var sen in sensorsListDto)
                    {
                        if (nsen.IdSensor == sen.IdSensor)
                        {
                            IsNew = false;
                            sensorsToDelete.Remove(sen);
                            break;
                        }
                    }
                    if (IsNew)
                    {
                        nsen.IdDispositivo = dto.IdDispositivo;
                        await _sensorService.CreateAsync(nsen);
                    }
                    else
                    {
                        await _sensorService.UpdateAsync(nsen.IdSensor, nsen);
                    }
                }
                foreach (var sensor in sensorsToDelete)
                {
                    var sen = item.Sensors.First(x => x.IdSensor == sensor.IdSensor);
                    /*if(sen.Medicions is not null) 
                    foreach(var med in sen.Medicions)
                    {
                        _context.Medicions.Remove(med);
                    }*/
                    var po = sen.PuntoOptimos?.Where(x => sensor.IdSensor == x.IdSensor);
                    if (po is not null)
                        foreach (var med in po)
                        {
                            _context.PuntoOptimos.Remove(med);
                        }
                    await _context.SaveChangesAsync();
                    _context.Sensors.Remove(sen);
                    await _context.SaveChangesAsync();

                }
            }
            if (dto.Sensors != null && dto.Sensors.Count != 0)
            {
                var pos = dto.Sensors.Last().PuntoOptimos;
                if (pos != null && pos.Count != 0)
                {
                    var listaexcesos = pos.Last().ExcesoPuntosOptimos;
                    if (pos != null && pos.Count != 0 && listaexcesos != null && listaexcesos.Count != 0)
                    {
                        List<ExcesoPuntoOptimo>? itlistexcT = null;
                        var itpos = item.Sensors.Last().PuntoOptimos;
                        if (itpos != null && itpos.Count != 0)
                            itlistexcT = [.. itpos.Last().ExcesoPuntosOptimos];
                        //if (itlistexc != null && itlistexc.Count != 0)
                        List<ExcesoPuntoOptimoDTO> itlistexc = itlistexcT!=null? _mapper.Map<List<ExcesoPuntoOptimoDTO>>(itlistexcT) : _mapper.Map<List<ExcesoPuntoOptimoDTO>>(new List<ExcesoPuntoOptimo>());
                        List<ExcesoPuntoOptimoDTO> itlistexct = [.. itlistexc.ToList()];
                        List<ExcesoPuntoOptimoDTO> newlistexc = [.. listaexcesos];
                        foreach (var nsen in newlistexc)
                        {
                            var IsNew = true;
                            foreach (var sen in itlistexct)
                            {
                                if (nsen.IdExcesoPuntoOptimo == sen.IdExcesoPuntoOptimo)
                                {
                                    IsNew = false;
                                    itlistexc.Remove(sen);
                                }
                            }
                            if (IsNew)
                            {
                                nsen.IdPuntoOptimo = pos.Last().IdPuntoOptimo;
                                await _excesoService.CreateAsync(nsen);
                            }
                            else
                            {
                                await _excesoService.UpdateAsync(nsen.IdExcesoPuntoOptimo, nsen);
                            }
                        }
                        foreach (var sensor in itlistexc)
                        {

                            await _excesoService.DeleteAsync(sensor.IdExcesoPuntoOptimo);

                        }

                    }
                }
            }
            if (dto.Actuadores != null)
            {

                List<ActuadorDTO> actuadors = _mapper.Map<List<ActuadorDTO>>(item.Actuadores.ToList());
                List<ActuadorDTO> actuadorsToDelete = [.. actuadors];
                foreach (var nsen in dto.Actuadores)
                {
                    var IsNew = true;
                    foreach (var sen in actuadors)
                    {
                        if (nsen.IdActuador == sen.IdActuador)
                        {
                            IsNew = false;
                            actuadorsToDelete.Remove(sen);
                        }
                    }
                    if (IsNew)
                    {
                        nsen.IdDispositivo = dto.IdDispositivo;
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
        }
    }
}
