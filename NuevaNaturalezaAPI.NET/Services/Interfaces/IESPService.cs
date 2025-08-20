using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IESPService
    {
        Task<List<ActuadorDTO>> GetActuadores();
        Task UpdateMedicions(List<MedicionDTO> mediciones);
    }
}
