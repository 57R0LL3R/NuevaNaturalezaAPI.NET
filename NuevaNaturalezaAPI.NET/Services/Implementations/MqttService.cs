using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _client;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public MqttService(IMqttClient client)
        {
            _client = client;
        }
        public async Task PublishAsync(string topic, string payload)
        {
            await _lock.WaitAsync();
            try
            {

                var options = new MqttClientOptionsBuilder()
                    .WithClientId("backend-api")
                    .WithTcpServer("188.93.149.80", 1883)
                    .WithCredentials("backend", "321")
                    .WithCleanSession(false)
                    .Build();

                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .WithRetainFlag(true)
                    .Build();

                if (!_client.IsConnected)
                {
                    await _client.ConnectAsync(options);
                }
                    await _client.PublishAsync(message);
            }
            finally
            {
                _lock.Release();
            }

        }
    }

}
