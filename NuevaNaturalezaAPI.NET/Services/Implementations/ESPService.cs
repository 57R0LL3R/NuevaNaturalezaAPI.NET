using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ESPService(NuevaNatuContext context, IMapper mapper,INotificacionService service, IHubContext<SignalRHub>  hubContext) : IESPService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly INotificacionService _service=service;

        private readonly IHubContext<SignalRHub> _hubContext = hubContext;
   
        public async Task<Response> Confirm(string estadosF)
        {
            var estados = estadosF.Split(",").ToList();
            var auditorias = await _context.Auditoria.Where(x => x.Estado == (int)NumberStatus.InProcces).OrderBy(x=>x.Fecha).ToListAsync();

            var actuadores = await _context.Actuador.Where(x => estados.Contains(x.On ?? string.Empty) || estados.Contains(x.Off ?? string.Empty)).ToListAsync();
            Actuador? act1 = null;
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
                            act1 = act;
                        }
                    }
                }
            }
            if(act1!=null)
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", new
            {
                tipo = "actuador",
                payload =
                    auditorias.Where(x => x.Estado != (int)NumberStatus.InProcces).ToList()

            });
            try
            {

                return new Response()
                {
                    Data = auditorias.Where(x => x.Estado != (int)NumberStatus.InProcces).ToList(),
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
                    outputs += auditorias[i].IdAccion.Value.Equals(typeAccions.FirstOrDefault(x=>x.IdAccionAct.CompareTo(Guid.Parse("80b8364b-8603-42d9-b857-0db5f055c6fd"))==0).IdAccionAct) ? act.On : act.Off;
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
                var dispositivos = await _context.Dispositivos.ToListAsync();
                var sensores = await _context.Sensors.ToListAsync();
                var Puntosoptimos = await _context.PuntoOptimos
                    .Include(x => x.IdSensorNavigation.IdDispositivoNavigation)
                    .Include(x => x.ExcesoPuntosOptimos)
                    .ThenInclude(x=>x.IdTipoExcesoNavigation)
                    .ToListAsync();

                // Cargamos actuadores y excesos una sola vez
                var actuadores = await _context.Actuador.ToListAsync();
                var excesosPuntoOptimoAll = await _context.ExcesoPuntoOptimo.ToListAsync();

                if (mediciones.DatosSensores is null)
                {
                    return new();
                }

                // Guardar fecha de medición
                FechaMedicion fm = new();
                if (mediciones.Fecha != null)
                {
                    fm.Fecha = ((DateTime)mediciones.Fecha).ToUniversalTime();
                }
                _context.FechaMedicions.Add(fm);
                await _context.SaveChangesAsync();

                // Para agrupar auditorías / notificaciones y luego guardar (puede guardarse dentro del loop si prefieres)
                foreach (var sensor in mediciones.DatosSensores)
                {
                    foreach (var kvp in sensor)
                    {
                        string nombre = kvp.Key;       // "nL"
                        var dis = dispositivos.FirstOrDefault(x => x.SegundoNombre?.Equals(nombre) ?? false);
                        var val = kvp.Value.ToString();
                        double valor = Convert.ToDouble(val, CultureInfo.InvariantCulture);

                        // Si no existe el dispositivo/sensor lo creamos (como tenías)
                        if (dis is null)
                        {
                            Dispositivo dis1 = new()
                            {
                                Nombre = nombre,
                                SegundoNombre = nombre,
                                Sensors = new List<Sensor>() { new Sensor() { /* inicializa si necesitas */ } }
                            };

                            _context.Dispositivos.Add(dis1);
                            await _context.SaveChangesAsync();

                            dis = dis1;
                            // recargar sensores list local (que usamos para buscar IdSensor)
                            sensores = await _context.Sensors.ToListAsync();
                        }

                        var idsen = sensores.First(x => x.IdDispositivo == dis.IdDispositivo).IdSensor;
                        Medicion m = new()
                        {
                            IdSensor = idsen,
                            IdFechaMedicion = fm.IdFechaMedicion,
                            Valor = valor,
                        };
                        _context.Medicions.Add(m);
                        await _context.SaveChangesAsync();

                        // Buscar punto óptimo para este sensor
                        var po = Puntosoptimos.FirstOrDefault(x => x.IdSensor == idsen);
                        if (po != null)
                        {
                            // si el valor está fuera del rango
                            if (m.Valor < po.ValorMin || m.Valor > po.ValorMax)
                            {
                                // 1) crear notificación (ya tenías esta lógica)
                                Notificacion notificacion = new()
                                {
                                    IdTipoNotificacion = tipoNoti.First(x => x.Nombre == "Valor fuera de rango").IdTipoNotificacion,
                                    IdTitulo = titulos.First(x => x.Titulo1 == "Sensor fuera de rango").IdTitulo,
                                    Enlace = "",
                                    Mensaje = "El sensor " + nombre + " tiene valores fuera del punto optimo (valor = " + valor + ")"
                                };
                                _context.Add(notificacion);
                                await _context.SaveChangesAsync();

                                // 2) buscar ExcesoPuntoOptimo asociados al punto óptimo actual

                                List<ExcesoPuntoOptimo>? excesos = null;
                                if (m.Valor < po.ValorMin)
                                    excesos = po.ExcesoPuntosOptimos.Where(x=>x.IdTipoExcesoNavigation.Nombre== "Inferior").ToList();
                                else
                                    excesos = po.ExcesoPuntosOptimos.Where(x => x.IdTipoExcesoNavigation.Nombre != "Inferior").ToList();

                                // Si no hay excesos definidos, aún podemos opcionalmente crear una auditoría genérica
                                if (excesos != null && excesos.Any())
                                {
                                    // Por cada ExcesoPuntoOptimo crear la auditoría correspondiente (una por actuador/acción)
                                    foreach (var exceso in excesos)
                                    {
                                        // Intentamos encontrar el actuador en el dispositivo objetivo que tenga la misma acción (IdAccionAct)
                                        var actuador = actuadores
                                            .FirstOrDefault(a => a.IdDispositivo == exceso.IdDispositivo
                                                              && a.IdAccionAct == exceso.IdAccionAct);

                                        // Si no encontramos un actuador con la acción exacta, intentamos cualquiera del dispositivo
                                        if (actuador == null)
                                        {
                                            actuador = actuadores.FirstOrDefault(a => a.IdDispositivo == exceso.IdDispositivo);
                                        }

                                        // Construir observación descriptiva
                                        string observ ="";
                                        if (actuador != null)
                                        {;
                                            observ = $" Exceso en Sensor {nombre} con valor {valor} fuera de rango ({po.ValorMin}-{po.ValorMax}). ";
                                        }

                                        // Crear registro de auditoría con estado InProcces para que la ESP lo lea luego
                                        var auditoria = new Auditorium()
                                        {
                                            IdDispositivo = exceso.IdDispositivo,
                                            IdAccion = exceso.IdAccionAct, // asociamos la acción que definió ExcesoPuntoOptimo
                                            Fecha = DateTime.UtcNow,
                                            Observacion = observ,
                                            Estado = (int)NumberStatus.InProcces
                                        };

                                        _context.Auditoria.Add(auditoria);
                                        await _context.SaveChangesAsync();

                                    }
                                }
                            } // fin if fuera de rango
                        } // fin if po != null
                    } // fin foreach kvp
                } // fin foreach sensor

                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", new
                {
                    tipo = "medicion",
                    payload =
                        ""

                });
                return new Response
                {
                    NumberResponse = (int)NumberResponses.Correct,
                    Message = "Sensores almacenados correctamente",
                    Data = mediciones
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    NumberResponse = (int)NumberResponses.Error
                };
            }
        }
    }
}
