using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IESPService
    {
        Task<string> GetOutsOfActuators();
        Task<Response> UpdateMedicions(MedicionesESP mediciones);
    }
}
