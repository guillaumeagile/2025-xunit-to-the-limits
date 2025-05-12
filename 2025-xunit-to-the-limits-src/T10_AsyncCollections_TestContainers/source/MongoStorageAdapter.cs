using System.Linq.Expressions;
using FluentResults;
using MongoDB.Driver;

namespace _2025_xunit_to_the_limits_src.T10_AsyncCollections_TestContainers.source;

public class MongoStorageAdapter<T>(MongoDbConnection? mongoDbConnection) : IStorageAdapter<T> where T : IDto
{
    private readonly IMongoCollection<T> _collection = mongoDbConnection.GetCollection<T>(typeof(T).Name);

    public async Task<Result<T>> GetByIdAsync(string id, CancellationToken token)
    {
        var result = await _collection.Find(dto => dto.Id == id).FirstOrDefaultAsync(token);

        if (result is not null)
            return Result.Ok(result);
        return Result.Fail($"No result found for ID: {id}");
    }


    public async Task<Result<IList<T>>> GetAllAsync(CancellationToken token)
    {
        try
        {
            IList<T>? results = await _collection.Find(_ => true).ToListAsync(token);
            return Result.Ok(results);
        }
        catch (Exception ex)
        {
            return Result.Fail<IList<T>>($"Failed to retrieve dataObject: {ex.Message}");
        }
    }

    public async Task<Result<T>> InsertOrUpdateAsync(T dataObject, CancellationToken token)
    {
        var filter = Builders<T>.Filter.Eq(dto => dto.Id, dataObject.Id);
        var options = new ReplaceOptions { IsUpsert = true };
       
          var result =   await _collection.ReplaceOneAsync(filter, dataObject, options, token);
          return result.IsAcknowledged ? Result.Ok() : Result.Fail("Update or Insert failed" );
    }

    
    public IQueryable<T> GetQueryable()
    {
        return _collection.AsQueryable();
    }


    public async Task<Result> UpdateOneFieldAsync<TField>(string id,
        Expression<Func<T, TField>> fieldExpression,
        TField updatedValue,
        CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
        var updateDefinition = Builders<T>.Update.Set<TField>(fieldExpression, updatedValue);
        try
        {
            var result = await _collection.UpdateOneAsync(filter, updateDefinition, null, cancellationToken);
            return result.ModifiedCount > 0 ? Result.Ok() : Result.Fail("Mise à jour échouée");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to update dataObject: {ex.Message}");
        }
    }


    public async Task<Result> DeleteAsync(string id, CancellationToken token)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq(dto => dto.Id, id);
            var deleteResult = await _collection.DeleteOneAsync(filter, token);

            if (deleteResult.DeletedCount > 0)
                return Result.Ok();

            return Result.Fail($"No dataObject found with ID: {id}");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to delete dataObject: {ex.Message}");
        }
    }
    
    public async Task<long> EstimatedCountAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _collection.EstimatedDocumentCountAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur EstimatedCountAsync : {ex.Message}");
            return 0;
        }
    }

 
}