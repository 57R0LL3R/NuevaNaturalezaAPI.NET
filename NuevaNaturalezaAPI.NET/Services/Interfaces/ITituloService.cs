using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ITituloService
    {
        Task<IEnumerable<TituloDTO>> GetAllAsync();
        Task<TituloDTO?> GetByIdAsync(Guid id);
        Task<TituloDTO?> CreateAsync(TituloDTO TituloDto);
        Task<bool> UpdateAsync(Guid id, TituloDTO TituloDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
