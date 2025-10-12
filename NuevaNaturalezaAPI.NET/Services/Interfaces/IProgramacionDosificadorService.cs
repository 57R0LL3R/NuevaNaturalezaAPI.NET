using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IProgramacionDosificadorService
    {
        Task<IEnumerable<ProgramacionDosificadorDTO>> GetAllAsync();
        Task<ProgramacionDosificadorDTO?> GetByIdAsync(Guid id);
        Task<ProgramacionDosificadorDTO?> CreateAsync(ProgramacionDosificadorDTO dto);
        Task<bool> UpdateAsync(Guid id, ProgramacionDosificadorDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

