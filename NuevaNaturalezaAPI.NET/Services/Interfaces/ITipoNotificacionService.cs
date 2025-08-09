using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ITipoNotificacionService
    {
        Task<IEnumerable<TipoNotificacionDTO>> GetAllAsync();
        Task<TipoNotificacionDTO?> GetByIdAsync(Guid id);
        Task<TipoNotificacionDTO?> CreateAsync(TipoNotificacionDTO dto);
        Task<bool> UpdateAsync(Guid id, TipoNotificacionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
