using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<RolDTO>> GetAllAsync();
        Task<RolDTO?> GetByIdAsync(Guid id);
        Task<RolDTO?> CreateAsync(RolDTO dto);
        Task<bool> UpdateAsync(Guid id, RolDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
