using System;
using System.Threading.Tasks;
using TransactionUploader.Application.Cache;

namespace TransactionUploader.Infrastructure.Cache.Redis
{
    public class RedisCacheManager: IRedisCacheManager
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> takeOn, int cacheTimeInMinutes = 60)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
    }
}
