using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IEstadoDispositivoService
    {
        Task<IEnumerable<EstadoDispositivoDTO>> GetAllAsync();
        Task<EstadoDispositivoDTO?> GetByIdAsync(Guid id);
        Task<EstadoDispositivoDTO?> CreateAsync(EstadoDispositivoDTO dto);
        Task<bool> UpdateAsync(Guid id, EstadoDispositivoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
