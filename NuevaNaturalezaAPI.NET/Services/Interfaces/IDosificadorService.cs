using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IDosificadorService
    {
        Task<IEnumerable<DosificadorDTO>> GetAllAsync();
        Task<DosificadorDTO?> GetByIdAsync(Guid id);
        Task<DosificadorDTO?> CreateAsync(DosificadorDTO dto);
        Task<bool> UpdateAsync(Guid id, DosificadorDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
