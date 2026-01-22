namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IMqttService
    {
        Task PublishAsync(string topic, string payload);
    }
}
