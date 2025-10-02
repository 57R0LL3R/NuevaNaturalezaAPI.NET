using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IExcesoPOService
    {
        Task<IEnumerable<ExcesoPuntoOptimoDTO>> GetAllAsync();
        Task<ExcesoPuntoOptimoDTO?> GetByIdAsync(Guid id);
        Task<ExcesoPuntoOptimoDTO?> CreateAsync(ExcesoPuntoOptimoDTO dto);
        Task<bool> UpdateAsync(Guid id, ExcesoPuntoOptimoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
