using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface IActuadorService
    {
        Task<IEnumerable<ActuadorDTO>> GetAllAsync();
        Task<ActuadorDTO?> GetByIdAsync(Guid id);
        Task<ActuadorDTO?> CreateAsync(ActuadorDTO dto);
        Task<bool> UpdateAsync(Guid id, ActuadorDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<Response?> ONOFFActuador(Guid id, ActuadorDTO dto, Guid? idSistema,string observacion);
    }
}
