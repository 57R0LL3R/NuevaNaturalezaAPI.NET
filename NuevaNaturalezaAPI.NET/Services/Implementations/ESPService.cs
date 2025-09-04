using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ESPService(NuevaNatuContext context, IMapper mapper,INotificacionService service) : IESPService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly INotificacionService _service=service;

        public async Task<Response> Confirm(string estadosF)
        {
            var estados = estadosF.Split(",").ToList();
            var auditorias = await _context.Auditoria.Where(x => x.Estado == (int)NumberStatus.InProcces).OrderBy(x=>x.Fecha).ToListAsync();
            var actuadores = await _context.Actuador.Where(x => estados.Contains(x.On ?? string.Empty) || estados.Contains(x.Off ?? string.Empty)).ToListAsync();
            foreach (Actuador act in actuadores)
            {
                foreach(string es in estados)
                {
                    if (act.Off == es || act.On == es)
                    {
                        var audi = auditorias.Find(x=>x.IdDispositivo.Equals(act.IdDispositivo));
                        if (audi != null)
                        {
                            act.IdAccionAct = audi.IdAccion;
                            audi.Estado = (int)NumberStatus.Correct;
                            _context.Entry(act).State = EntityState.Modified;
                            _context.Entry(audi).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                        }
                    }
                }
            }
            try
            {

                return new Response()
                {
                    Data = auditorias,
                    NumberResponse = (int)NumberResponses.Correct
                };
            }
            catch (Exception ex) {
                return new Response() { Message = ex.Message, NumberResponse = (int)NumberResponses.Error };
            }
        }

        public async Task<string> GetOutsOfActuators()
        {
            var auditorias = await _context.Auditoria.Where(x=>x.Estado==(int)NumberStatus.InProcces).ToListAsync();
            var actuadores = await _context.Actuador.ToListAsync();
            var typeAccions = await _context.AccionAct.ToListAsync();


            string outputs = "";
            for(int i = 0; i < auditorias.Count; i++)
            {
                var act = actuadores.Find(x => x.IdDispositivo == auditorias[i].IdDispositivo);
                if(act != null)
                {
                    outputs += auditorias[i].IdAccion.Value.Equals(typeAccions.FirstOrDefault(x=>x.Accion.Equals("Max")).IdAccionAct) ? act.On : act.Off;
                    outputs += ",";
                }
            }
            return outputs;
        }


        public async Task<Response> UpdateMedicions(MedicionesESP mediciones)
        {
            try
            {

                var titulos = await _context.Titulos.ToListAsync();
                var tipoNoti = await _context.TipoNotificacions.ToListAsync();
                var dispositivos = _context.Dispositivos.ToList();
                var sensores = _context.Sensors.ToList();
                var Puntosoptimos = await _context.PuntoOptimos.Include(x => x.IdSensorNavigation.IdDispositivoNavigation).ToListAsync();

                if (mediciones.DatosSensores is null) {
                    return new();
                }

                FechaMedicion fm;
                fm = new();
                _context.FechaMedicions.Add(fm);
                await _context.SaveChangesAsync();
                foreach (var sensor in mediciones.DatosSensores)
                {
                    foreach (var kvp in sensor)
                    {

                        string nombre = kvp.Key;       // "nL"
                        var dis = dispositivos.FirstOrDefault(x=>x.Nombre.Equals(nombre));
                        var val = kvp.Value.ToString();
                        double valor = Convert.ToDouble(val); // 100
                        if (dis is null)
                        {
                            Dispositivo dis1 = new()
                            {
                                Nombre = nombre,
                                Sensors= [new()]
                                
                            };

                            _context.Dispositivos.Add(dis1);
                            await _context.SaveChangesAsync();
                            dis = dis1;
                            sensores.Add(dis1.Sensors.First());

                        }

                        Medicion m = new()
                        {
                            IdSensor = sensores.First(x => x.IdDispositivo == dis.IdDispositivo).IdSensor,
                            IdFechaMedicion = fm.IdFechaMedicion,
                            Valor = valor,
                             
                        };
                        Console.WriteLine($"{nombre}: {valor}");
                        _context.Medicions.Add(m);
                        await _context.SaveChangesAsync();
                        foreach(var po in Puntosoptimos)
                        {
                            if (po.IdSensor.Equals(m.IdSensor))
                            {
                                if (m.Valor < po.ValorMin || m.Valor > po.ValorMax)
                                {
                                    Notificacion notificacion = new()
                                    {
                                        IdTipoNotificacion = tipoNoti.First(x => x.Nombre == "Valor fuera de rango").IdTipoNotificacion,
                                        IdTitulo = titulos.First(x => x.Titulo1 == "Sensor fuera de rango").IdTitulo,
                                        Enlace = "",
                                        Mensaje = "El sensor " + nombre + " tiene valores fuera del punto optimo (valor = " + valor + ")"
                                    };

                                    await _service.CreateAsync(_mapper.Map<NotificacionDTO>(notificacion));
                                    _context.Add(notificacion);
                                }
                            }
                        }
                        
                    }
                }



                


                return new Response
                {
                    NumberResponse = (int)NumberResponses.Correct,

                    Message = "Sensores almacenados correctamente",
                    Data = mediciones
                };
                


            }
            catch(Exception ex)
            {
                return new Response
                {
                    Message = ex.Message
                };
            }
        }
    }
}
