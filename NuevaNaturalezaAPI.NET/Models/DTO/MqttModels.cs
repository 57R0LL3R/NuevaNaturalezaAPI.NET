namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public record EstadoCmd(
    string Id,
    string Data
    );

    public record EstadoAck(
        string Id,
        bool Ok
    );

}
