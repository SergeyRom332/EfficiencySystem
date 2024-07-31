using Microsoft.Extensions.Caching.Memory;

namespace EfficiencySystem.Models.Services
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetCache<T>(object key)
        {
            return _cache.Get<T>(key);
        }

        public void AddCache<T>(object key, T value)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) });
        }

        public void DeleteCache(object key)
        {
            _cache.Remove(key);
        }
    }

    public static class CacheServiceProviderExtensions
    {
        public static void AddCacheService(this IServiceCollection services)
        {
            services.AddTransient<ICacheService, CacheService>();
        }
    }
}
