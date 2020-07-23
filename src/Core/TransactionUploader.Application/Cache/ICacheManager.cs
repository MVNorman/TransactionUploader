using System;
using System.Threading.Tasks;

namespace TransactionUploader.Application.Cache
{
    public interface ICacheManager: IDisposable
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> takeOn, int cacheTimeInMinutes = 60);
        void RemoveByPrefix(string prefix);
    }
}
