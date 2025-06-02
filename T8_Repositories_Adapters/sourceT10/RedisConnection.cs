using StackExchange.Redis;

namespace T8_Repositories_Adapters.sourceT10;

public class RedisConnection
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    public RedisConnection(string connectionString)
    {
        _redis = ConnectionMultiplexer.Connect(connectionString);
        _database = _redis.GetDatabase();
    }

    public IDatabase GetDatabase()
    {
        return _database;
    }

    public void Dispose()
    {
        _redis?.Dispose();
    }
}
