using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using NuGet.DependencyResolver;
using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NuevaNaturalezaAPI.NET.Services.BGService;
public class SyncBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISyncQueue _queue;

    private readonly IHubContext<SignalRHub> _hubContext;
    public SyncBackgroundService(
        IServiceScopeFactory scopeFactory,
        ISyncQueue queue,
        IHubContext<SignalRHub> hubContext)
    {
        _scopeFactory = scopeFactory;
        _queue = queue;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        Console.WriteLine("🔥 SyncBackgroundService INICIADO");
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("⏳ Esperando job...");
            var job = await _queue.DequeueAsync(stoppingToken);
            Console.WriteLine("📦 Job recibido");

            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NuevaNatuContext>();

            try
            {
                Console.WriteLine($"📥 Paquetes recibidos: {job.Sensores.Count}");
                await ProcesarSensoresAsync(context, job.Sensores,stoppingToken);
                Console.WriteLine("✅ Job procesado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("💥 ERROR EN BACKGROUND");
                Console.WriteLine(ex.ToString());

            }
        }
    }
    private bool TryGetDouble(object value, out double result)
    {
        result = 0;

        if (value is JsonElement je)
        {
            if (je.ValueKind == JsonValueKind.Number)
            {
                result = je.GetDouble();
                return true;
            }
            if (je.ValueKind == JsonValueKind.String &&
                double.TryParse(je.GetString(),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out result))
                return true;

            return false;
        }

        try
        {
            result = Convert.ToDouble(value, CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }


    }

    public async Task ProcesarSensoresAsync(NuevaNatuContext _context, List<List<Dictionary<string, object>>> dSensores, CancellationToken ct)
    {
        if (dSensores == null || dSensores.Count == 0)
        {

            return; }

        // ========================
        // 1. Cargar datos base UNA sola vez
        // ========================

        Console.WriteLine("cargando dispositivos");

        var dispositivos = await _context.Dispositivos
            .Include(d => d.Sensors)
            .ToListAsync(ct);
       var dispositivosByNombre = dispositivos
        .Where(d => !string.IsNullOrWhiteSpace(d.SegundoNombre))
        .GroupBy(d => d.SegundoNombre!)
        .ToDictionary(g => g.Key, g => g.First());

        var fechasExistentes = await _context.FechaMedicions
         .OrderByDescending(f => f.Fecha)
         .Take(500) // ajustable
         .Select(f => f.Fecha)
         .ToListAsync(ct);

        var fechasSet = new HashSet<long>(
            fechasExistentes.Select(f => f.Ticks)
        );
        const long toleranciaTicks = TimeSpan.TicksPerMinute;

        bool ExisteFecha(DateTime fecha)
        {
            var ticks = fecha.Ticks;

            return fechasSet.Any(t =>
                Math.Abs(t - ticks) <= toleranciaTicks);
        }


        var sensoresByDispositivo = dispositivos
        .SelectMany(d => d.Sensors)
        .GroupBy(s => s.IdDispositivoNavigation)
        .ToDictionary(g => g.Key, g => g.First());

        var sensoresByDispositivoRef = new Dictionary<Dispositivo, Sensor>();


        var titulos = await _context.Titulos.ToDictionaryAsync(t => t.Titulo1,ct);
        var tipoNoti = await _context.TipoNotificacions.ToDictionaryAsync(t => t.Nombre,ct);
        
        var puntosOptimos = (await _context.PuntoOptimos
        .Include(p => p.ExcesoPuntosOptimos)
            .ThenInclude(e => e.IdTipoExcesoNavigation)
        .ToListAsync(ct))
        .Where(p => p.IdSensor != null)
        .GroupBy(p => p.IdSensor!.Value)
        .ToDictionary(g => g.Key, g => g.First());


        var actuadores = await _context.Actuador.ToListAsync(ct);


        const int TAM_GRUPO = 12;

        var grupos = dSensores
            .Select((p, i) => new { p, i })
            .GroupBy(x => x.i / TAM_GRUPO)
            .Select(g => g.Select(x => x.p).ToList())
            .ToList();

        // ========================
        // 2. Detectar fecha UNA sola vez
        // ========================


        var mediciones = new List<Medicion>();
        var notificaciones = new List<Notificacion>();
        var auditorias = new List<Auditorium>();
        var eventos = new List<Evento>();
        var nuevosDispositivos = new List<Dispositivo>();
        var nuevasFechas = new List<FechaMedicion>();
        var anyGroupValide = false;
        foreach (var grupo in grupos)
        {
            DateTime fechaGrupo = default;
            var acumulados = new Dictionary<string, List<double>>();
            bool grupoValido = false;

            foreach (var paquete in grupo)
            {
                //if (paquete.Count == 0) continue;

                foreach (var bloque in paquete)
                {
                    if (bloque.TryGetValue("fecha", out var f))
                    {
                        var time = DateTime.Parse(f.ToString()!);
                        var zona = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

                        fechaGrupo = TimeZoneInfo.ConvertTimeToUtc(
                            DateTime.SpecifyKind(time, DateTimeKind.Unspecified),
                            zona);

                    }

                    if (fechaGrupo == default || ExisteFecha(fechaGrupo))
                    {
                        Console.WriteLine(
                            fechaGrupo == default
                            ? " Paquete sin fecha"
                            : $" Fecha duplicada: {fechaGrupo:o}"
                        );
                        continue;
                    }

                    foreach (var kvp in bloque)
                    {
                        if (kvp.Key == "fecha")
                            continue;

                        if (!TryGetDouble(kvp.Value, out var valor))
                            continue;

                        if (!acumulados.TryGetValue(kvp.Key, out var lista))
                            acumulados[kvp.Key] = lista = new List<double>();

                        lista.Add(valor);
                    }
                    Console.WriteLine($"fin grupo");
                    grupoValido = true;
                    fechasSet.Add(fechaGrupo.Ticks);
                    anyGroupValide = true;
                }



            }
            if (!grupoValido)
            {
                continue;
            }

            
            var fm = new FechaMedicion { Fecha = fechaGrupo };
            nuevasFechas.Add(fm);

            foreach (var kv in acumulados)
            {
                var nombreSensor = kv.Key;
                var promedio = kv.Value.Average();
                if (!dispositivosByNombre.TryGetValue(nombreSensor, out var dispositivo))
                {
                    dispositivo = new Dispositivo
                    {
                        Nombre = nombreSensor,
                        SegundoNombre = nombreSensor,
                        Sensors = new List<Sensor>()
                    };

                    nuevosDispositivos.Add(dispositivo);
                    dispositivosByNombre[nombreSensor] = dispositivo;
                }


                if (!sensoresByDispositivo.TryGetValue(dispositivo, out var sensor))
                {
                    sensor = new Sensor {IdDispositivoNavigation=dispositivo };
                    _context.Sensors.Add(sensor);
                    sensoresByDispositivo[dispositivo] = sensor;
                }

                // -------- Medición --------
                mediciones.Add(new Medicion
                {
                    IdSensorNavigation = sensor,
                    IdFechaMedicionNavigation = fm,
                    Valor = promedio
                });

                // --- Medición ---
                // -------- Punto óptimo --------
                if (!puntosOptimos.TryGetValue(sensor.IdSensor, out var po))
                    continue;

                if (promedio >= po.ValorMin && promedio <= po.ValorMax)
                    continue;

                // --- Notificación ---
                notificaciones.Add(new Notificacion
                {
                    IdTipoNotificacion = tipoNoti["Valor fuera de rango"].IdTipoNotificacion,
                    IdTitulo = titulos["Sensor fuera de rango"].IdTitulo,
                    Mensaje = $"{nombreSensor} fuera del punto óptimo ({promedio})"
                });

                var excesos = promedio < po.ValorMin
                    ? po.ExcesoPuntosOptimos?.Where(e => e.IdTipoExcesoNavigation!.Nombre == "Inferior")
                    : po.ExcesoPuntosOptimos?.Where(e => e.IdTipoExcesoNavigation!.Nombre != "Inferior");

                if (excesos == null) continue;

                foreach (var exceso in excesos)
                {
                    auditorias.Add(new Auditorium
                    {
                        IdDispositivo = exceso.IdDispositivo,
                        IdAccion = exceso.IdAccionAct,
                        Fecha = DateTime.UtcNow,
                        Estado = (int)NumberStatus.InProcces
                    });

                    eventos.Add(new Evento
                    {
                        IdImpacto = Guid.Parse("ec5e89b7-d35f-4925-900e-6dafe45e5470"),
                        IdAccionAct = exceso.IdAccionAct,
                        IdDispositivo = exceso.IdDispositivo,
                        IdSistema = Guid.Parse("1f1b289a-5fc7-426a-937c-1475c168d2f4")
                    });
                }
            }

        }
        if (grupos.Count == 0 || !anyGroupValide)
            return;


        _context.AddRange(nuevasFechas);
        _context.AddRange(nuevosDispositivos);
        _context.AddRange(mediciones);
        _context.AddRange(notificaciones);
        _context.AddRange(auditorias);
        _context.AddRange(eventos);


        await _context.SaveChangesAsync(ct);

        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", new
        {
            tipo = "medicion",
            payload =
                ""

        });
    }
}
