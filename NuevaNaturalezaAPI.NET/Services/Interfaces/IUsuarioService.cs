using NuevaNaturalezaAPI.NET.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO?> GetByIdAsync(Guid id);
        Task<UsuarioDTO?> CreateAsync(UsuarioDTO dto);
        Task<bool> UpdateAsync(Guid id, UsuarioDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
