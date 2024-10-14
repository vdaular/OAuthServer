using Newtonsoft.Json;
using OAuth2Server.Models.Config;
using StackExchange.Redis;

namespace OAuth2Server.Services.Cache;

public class RedisService : ICacheService
{
    private readonly string server = string.Empty;
    private ConnectionMultiplexer connection = null;
    private IServer redisServer = null;
    private IDatabase redisDb = null;

    public RedisService(IConfiguration config)
    {
        string? redisHost = config["Host:Redis"];

        if (string.IsNullOrWhiteSpace(redisHost))
            throw new NullReferenceException("Host:Redis configuration not set!");

        server = redisHost;
        Init();
    }

    public RedisService(IAppSettings appSettings)
    {
        string redisHost = appSettings.Host.Redis;

        if (string.IsNullOrEmpty(redisHost))
            throw new NullReferenceException("Host:Redis configuration not set!");

        server = redisHost;
        Init();
    }

    public RedisService(string redisHost)
    {
        server = redisHost;
        Init();
    }

    protected virtual void Init()
    {
        ConfigurationOptions options = ConfigurationOptions.Parse(server);
        options.ConnectTimeout = 30000;
        options.SyncTimeout = 30000;
        options.ConnectRetry = 2;

        connection = ConnectionMultiplexer.Connect(server);
        redisServer = connection.GetServer(options.EndPoints.First());
        redisDb = connection.GetDatabase();
    }

    public void ClearCache(string key)
    {
        if (redisDb.KeyExists(key))
            redisDb.KeyDelete(key);
    }

    public async Task ClearCacheAsync(string key)
    {
        if (redisDb.KeyExists(key))
            await redisDb.KeyDeleteAsync(key);
    }

    public async Task DecreaseAsync(string key, long by = 1)
    {
        await redisDb.StringDecrementAsync(key, by);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool GetCache<T>(string key, out T rtn) where T : new()
    {
        if (redisDb.KeyExists(key))
        {
            var value = redisDb.StringGet(key);
            rtn = JsonConvert.DeserializeObject<T>(value);

            return true;
        }
        else
        {
            rtn = default;

            return false;
        }
    }

    public async Task<Tuple<T, bool>> GetCacheAsync<T>(string key) where T : new()
    {
        if (await redisDb.KeyExistsAsync(key))
        {
            var value = await redisDb.StringGetAsync(key);

            return new Tuple<T, bool>(JsonConvert.DeserializeObject<T>(value), true);
        }
        else
            return new Tuple<T, bool>(default, false);
    }

    public async Task IncreaseAsync(string key, TimeSpan? expire = null, long by = 1)
    {
        if (!redisDb.KeyExists(key))
            await SaveCacheAsync(key, expire, 0);
        else
            await redisDb.KeyExpireAsync(key, expire);

        await redisDb.StringIncrementAsync(key, by);
    }

    public void SaveCache<T>(string key, T data)
    {
        string value = JsonConvert.SerializeObject(data);
        redisDb.StringSet(key, value);
    }

    public void SaveCache<T>(string key, TimeSpan? expire, T date)
    {
        var value = JsonConvert.SerializeObject(date);
        redisDb.StringSet(key, value, expiry: expire);
    }

    public async Task SaveCacheAsync<T>(string key, T data)
    {
        var value = JsonConvert.SerializeObject(data);
        await redisDb.StringSetAsync(key, value);
    }

    public async Task SaveCacheAsync<T>(string key, TimeSpan? expire, T data)
    {
        var value = JsonConvert.SerializeObject(data);
        await redisDb.StringSetAsync(key, value, expiry: expire);
        redisDb.KeyExpire(key, expire);
    }

    public IEnumerable<string> SearchKeys(string pattern = "")
    {
        if (string.IsNullOrEmpty(pattern))
            return redisServer.Keys().Select(x => x.ToString());
        else
        {
            var redisVal = (RedisValue)pattern;

            return redisServer.Keys(pattern: redisVal).Select(x => x.ToString());
        }
    }
}
