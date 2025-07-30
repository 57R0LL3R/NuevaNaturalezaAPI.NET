using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IMedicionService
    {
        Task<IEnumerable<MedicionDTO>> GetAllAsync();
        Task<MedicionDTO?> GetByIdAsync(Guid id);
        Task<MedicionDTO?> CreateAsync(MedicionDTO dto);
        Task<bool> UpdateAsync(Guid id, MedicionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
