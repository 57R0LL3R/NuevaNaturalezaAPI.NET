namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class LoginModel
    {
        public string? User { get; set; } = null!;
        public string? Pass { get; set; } = string.Empty;
        public string? Repass { get; set; } = string.Empty;
        public string? Url {  get; set; } = string.Empty;
    }
}
