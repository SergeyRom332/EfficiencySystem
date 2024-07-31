namespace EfficiencySystem.Models.Services
{
    public interface ICacheService
    {
        public T GetCache<T>(object key);
        public void AddCache<T>(object key, T value);
        public void DeleteCache(object key);
    }
}
