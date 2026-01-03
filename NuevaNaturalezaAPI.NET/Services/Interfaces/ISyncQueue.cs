using NuevaNaturalezaAPI.NET.Models.DTO;

namespace NuevaNaturalezaAPI.NET.Services.Interfaces
{
    public interface ISyncQueue
    {
        void Enqueue(SyncJob job);
        Task<SyncJob> DequeueAsync(CancellationToken ct);
    }

}
