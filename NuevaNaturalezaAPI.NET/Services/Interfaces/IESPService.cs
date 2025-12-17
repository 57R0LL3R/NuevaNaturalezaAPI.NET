using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IESPService
    {
        Task<string> GetOutsOfActuators();
        Task<Response> UpdateMedicions(MedicionesESP mediciones);
        Task<Response> Sincronizacion(List<Dictionary<string, object>>? dSensores);
        Task<Response> Confirm(string estadosf);
        Task<Response> Confirm2(string estadosf); 
    }
}
