using MongoDB.Driver;

namespace _2025_xunit_to_the_limits_src.T10_AsyncCollections_TestContainers.source;

public class MongoDbConnection
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoDbConnection(string connectionString, string databaseName)
    {
        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}