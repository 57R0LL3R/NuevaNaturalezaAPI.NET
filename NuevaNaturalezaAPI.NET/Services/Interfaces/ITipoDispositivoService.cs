using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ITipoDispositivoService
    {
        Task<IEnumerable<TipoDispositivoDTO>> GetAllAsync();
        Task<TipoDispositivoDTO?> GetByIdAsync(Guid id);
        Task<TipoDispositivoDTO> CreateAsync(TipoDispositivoDTO dto);
        Task<bool> UpdateAsync(Guid id, TipoDispositivoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
