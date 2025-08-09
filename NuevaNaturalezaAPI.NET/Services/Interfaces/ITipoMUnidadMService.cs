using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ITipoMUnidadMService
    {
        Task<IEnumerable<TipoMUnidadMDTO>> GetAllAsync();
        Task<TipoMUnidadMDTO?> GetByIdAsync(Guid id);
        Task<TipoMUnidadMDTO?> CreateAsync(TipoMUnidadMDTO dto);
        Task<bool> UpdateAsync(Guid id, TipoMUnidadMDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
