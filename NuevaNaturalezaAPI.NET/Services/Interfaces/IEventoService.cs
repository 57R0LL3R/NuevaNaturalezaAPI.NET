using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<EventoDTO>> GetAllAsync();
        Task<EventoDTO?> GetByIdAsync(Guid id);
        Task<EventoDTO?> CreateAsync(EventoDTO dto);
        Task<bool> UpdateAsync(Guid id, EventoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
