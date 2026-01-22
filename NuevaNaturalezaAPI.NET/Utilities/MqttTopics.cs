namespace NuevaNaturalezaAPI.NET.Utilities
{
    public static class MqttTopics
    {
        public static string Cmd(string deviceId)
            => $"devices/{deviceId}/cmd";

        public static string Ack(string deviceId)
            => $"devices/{deviceId}/ack";
    }

}
