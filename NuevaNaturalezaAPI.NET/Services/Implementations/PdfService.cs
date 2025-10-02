using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Linq;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class PdfService(NuevaNatuContext _context, IMapper mapper,IDispositivoService DService,IAuditoriumService AService,IEventoService EService):IPdfService
    {
        private readonly NuevaNatuContext _context=_context;
        private readonly IMapper _mapper = mapper;
        private readonly IDispositivoService _dService = DService;
        private readonly IAuditoriumService _aService = AService;
        private readonly IEventoService _eService = EService;
        public async Task<Response> PdfSensor(PdfQuery pdfSens)
        {
            var dispositivos = await _context.Dispositivos
                .Include(d => d.IdTipoDispositivoNavigation)
                .Include(d => d.IdMarcaNavigation)
                .Include(d => d.Actuadores)
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
                        .ThenInclude(t => t.IdUnidadMedidaNavigation)
                .ToListAsync();

            pdfSens.Hasta = pdfSens.Hasta.AddDays(1);
            var list1 =  _mapper.Map<List<DispositivoDTO>>(dispositivos);
            foreach (var dispositivo in list1)
            {
                foreach (var sensor in dispositivo.Sensors)
                {
                    if(sensor.Medicions !=null)
                    sensor.Medicions = [.. sensor.Medicions
                        .OrderBy(m => m.Fecha)
                        .Where(x => x.Fecha > pdfSens.Desde.Date && x.Fecha < pdfSens.Hasta.Date)];
                }
            }

            Response res = new()
            {
                NumberResponse = (int)NumberResponses.Correct,
                Data = list1

            };
            return res;
        }

        public async Task<Response> PdfAuditoria(PdfQuery pdfAudi)
        {
            var Auditorias = await _aService.GetAllAsync();
            pdfAudi.Hasta = pdfAudi.Hasta.AddDays(1);
            Auditorias = [.. Auditorias.OrderBy(m => m.Fecha).Where(x => x.Fecha > pdfAudi.Desde.Date && x.Fecha < pdfAudi.Hasta.Date)];
                
            

            Response res = new()
            {
                NumberResponse = (int)NumberResponses.Correct,
                Data = Auditorias

            };
            return res;
        }
        public async Task<Response> PdfEvento(PdfQuery pdfEven)
        {
            var Eventos = await _eService.GetAllAsync();
            pdfEven.Hasta = pdfEven.Hasta.AddDays(1);
            Eventos = [.. Eventos.OrderBy(m => m.FechaEvento)
                .Where(x => x.FechaEvento > pdfEven.Desde.Date && x.FechaEvento < pdfEven.Hasta.Date)];



            Response res = new()
            {
                NumberResponse = (int)NumberResponses.Correct,
                Data = Eventos

            };
            return res;
        }

    }
}
