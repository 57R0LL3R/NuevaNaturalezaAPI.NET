using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IDispositivoService
    {
        Task<IEnumerable<DispositivoDTO>> GetAllAsync();
        Task<DispositivoDTO?> GetByIdAsync(Guid id);
        Task<DispositivoDTO?> CreateAsync(DispositivoDTO dto);
        Task<bool> UpdateAsync(Guid id, DispositivoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
