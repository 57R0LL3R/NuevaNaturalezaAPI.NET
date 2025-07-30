using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IPuntoOptimoService
    {
        Task<IEnumerable<PuntoOptimoDTO>> GetAllAsync();
        Task<PuntoOptimoDTO?> GetByIdAsync(Guid id);
        Task<PuntoOptimoDTO?> CreateAsync(PuntoOptimoDTO dto);
        Task<bool> UpdateAsync(Guid id, PuntoOptimoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
