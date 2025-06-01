using MongoDB.Driver;

namespace T8_Repositories_Adapters.source;

public class MongoDbConnection
{
    private readonly IMongoDatabase _database;

    public MongoDbConnection(string connectionString, string databaseName)
    {
        IMongoClient client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}