using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using TransactionUploader.Application.Cache;

namespace TransactionUploader.Infrastructure.Cache
{
    public sealed class MemoryCacheManager : IMemoryCacheManager
    {
        private static readonly CancellationTokenSource ClearToken = new CancellationTokenSource();
        private static readonly ConcurrentDictionary<string, CancellationTokenSource> Prefixes =
            new ConcurrentDictionary<string, CancellationTokenSource>();

        private readonly IMemoryCache _memoryCache;
        private bool _disposed;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> takeOn, int cacheTimeInMinutes = 60)
        {
            var result = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                Prefixes.GetOrAdd(key, new CancellationTokenSource());

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTimeInMinutes);
                entry.AddExpirationToken(new CancellationChangeToken(ClearToken.Token));

                return await takeOn();
            });

            if (result == null)
                Remove(key);

            return result;
        }

        public void RemoveByPrefix(string prefix)
        {
            var keys = Prefixes
                .Where(x => x.Key.StartsWith(prefix))
                .Select(x=>x.Key);

            foreach (var key in keys)
                _memoryCache.Remove(key);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _memoryCache.Dispose();
            }

            _disposed = true;
        }

        private void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
