namespace NuevaNaturalezaAPI.NET.Services.BGService
{
    using MQTTnet;
    using MQTTnet.Client;
    using MQTTnet.Packets;
    using MQTTnet.Protocol;
    using NuevaNaturalezaAPI.NET.Models.DTO;
    using NuevaNaturalezaAPI.NET.Services.Implementations;
    using NuevaNaturalezaAPI.NET.Services.Interfaces;
    using NuevaNaturalezaAPI.NET.Utilities;
    using System.Text;
    using System.Text.Json;
    using static System.Net.Mime.MediaTypeNames;

    public class MqttBackgroundService(IMqttClient client, IServiceScopeFactory scopeFactory, ILogger<MqttBackgroundService> logger) : BackgroundService
    {

        private readonly IMqttClient _client = client;
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<MqttBackgroundService> _logger = logger;

        async Task ProcesarEstadoProcesado(string valor,IESPService _espService)
        {
            await _espService.Confirm(valor);
        }

         async Task ProcesarConfirmacion(string valor, IESPService _espService)
        {
            await _espService.Confirm2(valor);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId("backend-api")
                .WithTcpServer("188.93.149.80", 1883)
                .WithCredentials("backend", "321")
                .WithCleanSession(false)
                .Build();


            _client.DisconnectedAsync += async e =>
            {
                _logger.LogWarning("MQTT desconectado. Reintentando...");

                while (!_client.IsConnected && !stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                        await _client.ConnectAsync(options, stoppingToken);
                        _logger.LogInformation("MQTT reconectado");
                    }
                    catch
                    {
                        _logger.LogWarning("Reintento fallido");
                    }
                }
            };

            _client.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                if (e.ApplicationMessage.Topic == "nn/actuadores/ack")
                {
                    Console.WriteLine("✔ ACK recibido: " + payload);
                    // aquí puedes confirmar en DB
                    try
                    {

                        using var scope = _scopeFactory.CreateScope();
                        var espService = scope.ServiceProvider.GetRequiredService<IESPService>();

                        var msg = JsonSerializer.Deserialize<MqttCmdDto>(payload);

                        if (msg == null || string.IsNullOrEmpty(msg.t))
                            return;// Task.CompletedTask;
                        List<string> tiposToConfirm = ["ea", "ep"];
                        if (tiposToConfirm.Contains(msg.t))
                        {
                            await SendCommand("{\"t\": \"" + msg.t
                                + "\",\"status\":\"ok\",\"id\":\"" + (msg.id ?? "") + "\"}");
                        }
                        switch (msg.t)
                        {
                            case "ep":
                                Console.WriteLine($" Confirm2 (estado procesado): {msg.d}");
                                await ProcesarEstadoProcesado(msg.d,espService);
                                break;

                            case "ea":
                                Console.WriteLine($" Confirm (estado actuadores): {msg.d}");
                                await ProcesarConfirmacion(msg.d, espService);
                                break;

                            default:
                                Console.WriteLine("⚠ Tipo desconocido: " + msg.t);
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("❌ Error procesando MQTT CMD: " + ex.Message);
                    }
                }

                return;// Task.CompletedTask;
            };

            await _client.ConnectAsync(options, stoppingToken);

            await _client.SubscribeAsync(
                new MqttTopicFilterBuilder()
                    .WithTopic("nn/actuadores/ack")
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build()
            );

            Console.WriteLine("🟢 Backend MQTT conectado");

            // Ejemplo: enviar comando

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task SendCommand(string cmd)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("nn/actuadores/cmd")
                .WithPayload(cmd)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag(true)
                .Build();

            await _client.PublishAsync(message);
            Console.WriteLine("📤 CMD enviado: " + cmd);
        }
        
    }

}
