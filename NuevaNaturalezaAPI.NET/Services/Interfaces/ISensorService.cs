using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ISensorService
    {
        Task<IEnumerable<SensorDTO>> GetAllAsync();
        Task<SensorDTO?> GetByIdAsync(Guid id);
        Task<SensorDTO?> CreateAsync(SensorDTO dto);
        Task<bool> UpdateAsync(Guid id, SensorDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
