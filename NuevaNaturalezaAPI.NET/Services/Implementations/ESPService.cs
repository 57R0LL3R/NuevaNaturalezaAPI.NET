using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ESPService(NuevaNatuContext context, IMapper mapper) : IESPService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<List<ActuadorDTO>> GetActuadores()
        {
            var list = await _context.Auditoria.Include(x => x.IdDispositivoNavigation).ToListAsync();
            var listf = _mapper.Map<List<AuditoriumDTO>>(list);

            var listf1 = await _context.Actuador.Include(x => x.IdDispositivoNavigation).ToListAsync();
            return _mapper.Map<List<ActuadorDTO>>(listf1);
        }


        public async Task<Response> UpdateMedicions(List<MedicionDTO> mediciones)
        {
            try
            {

                List<Medicion> medicionesR = _mapper.Map<List<Medicion>>(mediciones);

                var sensores = await _context.Sensors.ToListAsync();
                bool reales = false;
                for(int i0 = 0; i0 < medicionesR.Count; i0++)
                {
                    for (int i1 = 0;i1 < sensores.Count; i1++)
                    {
                        if (medicionesR[i0].IdSensor == sensores[i1].IdSensor)
                        {
                            reales = true;
                            break;
                        }

                    }
                    if (!reales)
                    {
                        break;
                    }
                    reales = false;
                }
                if (reales)
                {
                    var Puntosoptimos = await _context.PuntoOptimos.Include(x => x.IdSensorNavigation.IdDispositivoNavigation).ToListAsync();
                    var titulos = await _context.Titulos.ToListAsync();
                    var tipoNoti = await _context.TipoNotificacions.ToListAsync();
                    FechaMedicion fm;
                    fm = new();
                    await _context.SaveChangesAsync();


                    for (int i = 0; i < medicionesR.Count; i++)
                    {
                        for (int j = 0; j < Puntosoptimos.Count; j++)
                        {
                            if (Puntosoptimos[j].IdSensor.Equals(medicionesR[i].IdSensor))
                            {
                                if (medicionesR[i].Valor < Puntosoptimos[j].ValorMin || medicionesR[i].Valor > Puntosoptimos[j].ValorMax)
                                {
                                    Notificacion notificacion = new()
                                    {
                                        IdTipoNotificacion = tipoNoti.First(x => x.Nombre == "Valor fuera de rango").IdTipoNotificacion,
                                        IdTitulo = titulos.First(x => x.Titulo1 == "Sensor fuera de rango").IdTitulo,
                                        Enlace = "",
                                        Mensaje = "El sensor " + medicionesR[i].IdSensor + " tiene valores fuera del punto optimo (valor = " + medicionesR[j].Valor + ")"
                                    };
                                    _context.Add(notificacion);
                                }
                            }
                        }
                        medicionesR[i].IdFechaMedicion = fm.IdFechaMedicion;
                        medicionesR[i].IdUnidadMedida = sensores.First(x => x.IdSensor == medicionesR[i].IdSensor).IdUnidadMedida;
                        _context.Add(medicionesR[i]);

                    }

                    await _context.SaveChangesAsync();

                    return new Response
                    {
                        NumberResponse = (int)NumberResponses.Correct,

                        Message = "Sensores almacenados correctamente",
                        Data = medicionesR
                    };
                }

                return new Response
                {
                    Message = "No son sensores reales",
                    Data = medicionesR
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
