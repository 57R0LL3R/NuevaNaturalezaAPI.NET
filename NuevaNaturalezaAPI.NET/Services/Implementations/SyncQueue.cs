using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System.Threading.Channels;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class SyncQueue : ISyncQueue
    {
        private readonly Channel<SyncJob> _queue = Channel.CreateUnbounded<SyncJob>();

        public void Enqueue(SyncJob job)
        {
            _queue.Writer.TryWrite(job);
        }

        public async Task<SyncJob> DequeueAsync(CancellationToken ct)
        {
            return await _queue.Reader.ReadAsync(ct);
        }
    }

}
