using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IAuditoriumService
    {
        Task<IEnumerable<AuditoriumDTO>> GetAllAsync();
        Task<AuditoriumDTO?> GetByIdAsync(Guid id);
        Task<AuditoriumDTO?> CreateAsync(AuditoriumDTO dto);
        Task<bool> UpdateAsync(Guid id, AuditoriumDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
