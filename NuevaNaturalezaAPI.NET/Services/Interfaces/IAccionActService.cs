using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IAccionActService
    {
        Task<IEnumerable<AccionActDTO>> GetAllAsync();
        Task<AccionActDTO?> GetByIdAsync(Guid id);
        Task<AccionActDTO?> CreateAsync(AccionActDTO dto);
        Task<bool> UpdateAsync(Guid id, AccionActDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
