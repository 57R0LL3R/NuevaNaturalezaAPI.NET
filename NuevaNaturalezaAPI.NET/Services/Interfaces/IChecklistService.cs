using NuevaNaturalezaAPI.NET.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IChecklistService
    {
        Task<IEnumerable<ChecklistDTO>> GetAllAsync();
        Task<ChecklistDTO?> GetByIdAsync(Guid id);
        Task<ChecklistDTO?> CreateAsync(ChecklistDTO dto);
        Task<bool> UpdateAsync(Guid id, ChecklistDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

