using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDTO>> GetAllAsync();
        Task<AreaDTO?> GetByIdAsync(Guid id);
        Task<AreaDTO?> CreateAsync(AreaDTO dto);
        Task<bool> UpdateAsync(Guid id, AreaDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
