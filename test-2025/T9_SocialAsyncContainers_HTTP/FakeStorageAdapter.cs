using System.Collections.Concurrent;
using System.Linq.Expressions;
using FluentResults;
using NUnit.Framework;
using T8_Repositories_Adapters.source;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

public class FakeStorageAdapter<T> : IStorageAdapter<T> where T : IDto
{
    private readonly ConcurrentDictionary<string, T> _storage = new();
    public Task<Result<T>> GetByIdAsync(string id, CancellationToken token)
    {
        _storage.TryGetValue(id, out var result);
        if (result is null)
            return Task.FromResult(Result.Fail<T>($"No result found for ID: {id}"));
        return Task.FromResult(Result.Ok(result));
    }

    public Task<Result<IList<T>>> GetAllAsync(CancellationToken token)
    {
        var results = _storage.Values.ToList() as IList<T>;
        return Task.FromResult(Result.Ok(results)); 
    }

    public Task<Result<T>> InsertOrUpdateAsync(T dataObject, CancellationToken token)
    {
        _storage[dataObject.Id] = dataObject;
        return Task.FromResult(Result.Ok(dataObject)); 
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
        return Task.FromResult(  (long)_storage.Count); 
    }
}