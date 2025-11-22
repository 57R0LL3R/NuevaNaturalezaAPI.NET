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

            var Desde1 = pdfSens.Desde.ToUniversalTime();
            var now = DateTime.UtcNow;
            var Hasta2 = pdfSens.Hasta.AddDays(1).ToUniversalTime();
            var dispositivos = await _context.Dispositivos
                .Include(d => d.IdTipoDispositivoNavigation)
                .Include(d => d.IdMarcaNavigation)
                .Include(d => d.Actuadores)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                        .ThenInclude(t => t.IdTipoMedicionNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.IdTipoMUnidadMNavigation)
                        .ThenInclude(t => t.IdUnidadMedidaNavigation)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.PuntoOptimos)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.Medicions
                    .Where(m =>( m.IdFechaMedicionNavigation.Fecha > Desde1 && m.IdFechaMedicionNavigation.Fecha < Hasta2) && m.IdFechaMedicionNavigation.Fecha < now))
                    .ThenInclude(m => m.IdFechaMedicionNavigation)
                    .Where(d=> (pdfSens.IdsDispositivos==null || pdfSens.IdsDispositivos.Length==0) ? true: pdfSens.IdsDispositivos.Any(id=> id.Equals(d.IdDispositivo.ToString()))  )
                
                .ToListAsync();


            pdfSens.Hasta = pdfSens.Hasta.AddDays(1).ToUniversalTime();
            pdfSens.Desde = pdfSens.Desde.Date.ToUniversalTime();
            var list1 =  _mapper.Map<List<DispositivoDTO>>(dispositivos);
            foreach (var dispositivo in list1)
            {
                foreach (var sensor in dispositivo.Sensors)
                {
                    if(sensor.Medicions !=null)
                    sensor.Medicions = [.. sensor.Medicions
                        .OrderBy(m => m.Fecha)
                        ];
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

            var Desde1 = pdfAudi.Desde.ToUniversalTime();

            var Hasta2 = pdfAudi.Hasta.AddDays(1).ToUniversalTime();
            Auditorias = [.. Auditorias.OrderBy(m => m.Fecha).Where(x => x.Fecha > Desde1 && x.Fecha < Hasta2)];
                
            

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

            var Desde1 = pdfEven.Desde.ToUniversalTime();

            var Hasta2 = pdfEven.Hasta.AddDays(1).ToUniversalTime();
            Eventos = [.. Eventos.OrderBy(m => m.FechaEvento)
                .Where(x => x.FechaEvento > Desde1 && x.FechaEvento < Hasta2)];



            Response res = new()
            {
                NumberResponse = (int)NumberResponses.Correct,
                Data = Eventos

            };
            return res;
        }

    }
}
