using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ISugerenciaService
    {
        Task<IEnumerable<SugerenciaDTO>> GetAllAsync();
        Task<SugerenciaDTO?> GetByIdAsync(Guid id);
        Task<SugerenciaDTO?> CreateAsync(SugerenciaDTO dto);
        Task<bool> UpdateAsync(Guid id, SugerenciaDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
