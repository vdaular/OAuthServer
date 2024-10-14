namespace OAuth2Server.Services.Cache;

public interface ICacheService : IDisposable
{
    IEnumerable<string> SearchKeys(string pattern = "");

    void SaveCache<T>(string key, T data);

    void SaveCache<T>(string key, TimeSpan? expire, T date);

    Task SaveCacheAsync<T>(string key, T data);

    Task SaveCacheAsync<T>(string key, TimeSpan? expire, T data);

    Task IncreaseAsync(string key, TimeSpan? expire = null, long by = 1);

    Task DecreaseAsync(string key, long by = 1);

    bool GetCache<T>(string key, out T rtn) where T : new();

    Task<Tuple<T, bool>> GetCacheAsync<T>(string key) where T : new();

    void ClearCache(string key);

    Task ClearCacheAsync(string key);
}
