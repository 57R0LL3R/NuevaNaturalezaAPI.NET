using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IImpactoService
    {
        Task<IEnumerable<ImpactoDTO>> GetAllAsync();
        Task<ImpactoDTO?> GetByIdAsync(Guid id);
        Task<ImpactoDTO?> CreateAsync(ImpactoDTO dto);
        Task<bool> UpdateAsync(Guid id, ImpactoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
