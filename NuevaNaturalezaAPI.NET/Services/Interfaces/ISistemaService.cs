using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ISistemaService
    {
        Task<IEnumerable<SistemaDTO>> GetAllAsync();
        Task<SistemaDTO?> GetByIdAsync(Guid id);
        Task<SistemaDTO> CreateAsync(SistemaDTO sistemaDto);
        Task<bool> UpdateAsync(Guid id, SistemaDTO sistemaDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
