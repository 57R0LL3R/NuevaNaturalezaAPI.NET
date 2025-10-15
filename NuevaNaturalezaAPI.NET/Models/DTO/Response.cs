enum NumberResponses
{
    Correct, Warning, Error,
    Incorrect
}

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class Response
    {
        public int NumberResponse { get; set; } = (int)NumberResponses.Error;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
