using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDTO>> GetAllAsync();
        Task<MarcaDTO?> GetByIdAsync(Guid id);
        Task<MarcaDTO?> CreateAsync(MarcaDTO dto);
        Task<bool> UpdateAsync(Guid id, MarcaDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
