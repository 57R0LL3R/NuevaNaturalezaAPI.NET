using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IFechaMedicionService
    {
        Task<IEnumerable<FechaMedicionDTO>> GetAllAsync();
        Task<FechaMedicionDTO?> GetByIdAsync(Guid id);
        Task<FechaMedicionDTO?> CreateAsync(FechaMedicionDTO dto);
        Task<bool> UpdateAsync(Guid id, FechaMedicionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
