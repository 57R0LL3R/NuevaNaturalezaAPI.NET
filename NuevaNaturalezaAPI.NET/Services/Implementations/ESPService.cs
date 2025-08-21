using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System.Collections.Generic;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ESPService(NuevaNatuContext context, IMapper mapper) : IESPService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<List<ActuadorDTO>> GetActuadores()
        {
            return _mapper.Map<List<ActuadorDTO>>(await _context.Actuador.Include(x=>x.IdDispositivoNavigation).ToListAsync());
        }


        public async Task UpdateMedicions(List<MedicionDTO> mediciones)
        {
            List<Medicion> medicionesR = _mapper.Map<List<Medicion>>(mediciones);
            var Puntosoptimos = await _context.PuntoOptimos.Include(x=>x.IdSensorNavigation.IdDispositivoNavigation).ToListAsync();
            var titulos = await _context.Titulos.ToListAsync();
            var tipoNoti = await _context.TipoNotificacions.ToListAsync();
            if (mediciones.Count > 0)
            {
                FechaMedicion fm = new FechaMedicion();
            }

            for (int i = 0; i < medicionesR.Count; i++)
            {
                for (int j = 0; j < Puntosoptimos.Count; j++)
                {
                    if (Puntosoptimos[j].IdSensor.Equals(medicionesR[i].IdSensor))
                    {
                        if(medicionesR[i].Valor < Puntosoptimos[j].ValorMin || medicionesR[i].Valor > Puntosoptimos[j].ValorMax)
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
                _context.Add(medicionesR[i]);

            }

            return;
        }
    }
}
