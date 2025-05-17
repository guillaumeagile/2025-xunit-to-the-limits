using System.Linq.Expressions;
using System.Text.Json;
using _2025_xunit_to_the_limits_src.T8_AsyncCollections_TestContainers.source;
using FluentResults;
using StackExchange.Redis;

namespace _2025_xunit_to_the_limits_src.T11_Redis_ToBenchmark.source;

public class RedisStorageAdapter<T>(RedisConnection redisConnection) : IStorageAdapter<T> where T : IDto
{
    private readonly IDatabase _database = redisConnection.GetDatabase();
    private readonly string _keyPrefix = typeof(T).Name + ":";

    public async Task<Result<T>> GetByIdAsync(string id, CancellationToken token)
    {
        try
        {
            // Construct the key using the type name prefix and the id
            string key = _keyPrefix + id;
            
            // Get the value from Redis
            RedisValue value = await _database.StringGetAsync(key);
            
            // Check if the value exists
            if (value.IsNullOrEmpty)
                return Result.Fail<T>($"No result found for ID: {id}");
            
            // Deserialize the JSON string to the object
            T? result = JsonSerializer.Deserialize<T>(value.ToString());
            
            // Return success or failure based on deserialization result
            return result != null 
                ? Result.Ok(result) 
                : Result.Fail<T>($"Failed to deserialize object with ID: {id}");
        }
        catch (Exception ex)
        {
            // Return failure with exception message
            return Result.Fail<T>($"Failed to retrieve object: {ex.Message}");
        }
    }

    public async Task<Result<IList<T>>> GetAllAsync(CancellationToken token)
    {
        throw new NotImplementedException();    
    }
    
    public async Task<Result<T>> InsertOrUpdateAsync(T dataObject, CancellationToken token)
    {
        try
        {
            string key = _keyPrefix + dataObject.Id;
            string json = JsonSerializer.Serialize(dataObject);

            await _database.StringSetAsync(key, json, TimeSpan.FromDays(1), when: When.Always, flags: CommandFlags.FireAndForget);

            return Result.Ok(dataObject);
        }
        catch (Exception ex)
        {
            return Result.Fail<T>($"Failed to insert or update dataObject: {ex.Message}");
        }
    }

    public Task<Result> DeleteAsync(string id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetQueryable()
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateOneFieldAsync<TField>(string id, Expression<Func<T, TField>> fieldExpression, TField updatedValue,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<long> EstimatedCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}