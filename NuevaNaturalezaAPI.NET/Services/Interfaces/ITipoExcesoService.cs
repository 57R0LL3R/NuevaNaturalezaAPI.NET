using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ITipoExcesoService
    {
        Task<IEnumerable<TipoExcesoDTO>> GetAllAsync();
        Task<TipoExcesoDTO?> GetByIdAsync(Guid id);
        Task<TipoExcesoDTO?> CreateAsync(TipoExcesoDTO dto);
        Task<bool> UpdateAsync(Guid id, TipoExcesoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
