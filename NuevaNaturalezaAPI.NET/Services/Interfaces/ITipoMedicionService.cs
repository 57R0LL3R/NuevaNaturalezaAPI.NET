using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ITipoMedicionService
    {
        Task<IEnumerable<TipoMedicionDTO>> GetAllAsync();
        Task<TipoMedicionDTO?> GetByIdAsync(Guid id);
        Task<TipoMedicionDTO?> CreateAsync(TipoMedicionDTO dto);
        Task<bool> UpdateAsync(Guid id, TipoMedicionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
