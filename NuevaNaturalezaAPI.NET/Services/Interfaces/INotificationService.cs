using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface INotificacionService
    {
        Task<IEnumerable<NotificacionDTO>> GetAllAsync();
        Task<NotificacionDTO?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, NotificacionDTO dto);
        Task<NotificacionDTO?> CreateAsync(NotificacionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

