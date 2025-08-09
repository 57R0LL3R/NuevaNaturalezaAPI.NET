using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IUnidadMedidaService
    {
        Task<IEnumerable<UnidadMedidumDTO>> GetAllAsync();
        Task<UnidadMedidumDTO?> GetByIdAsync(Guid id);
        Task<UnidadMedidumDTO?> CreateAsync(UnidadMedidumDTO dto);
        Task<bool> UpdateAsync(Guid id, UnidadMedidumDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
