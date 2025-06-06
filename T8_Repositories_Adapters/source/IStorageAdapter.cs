using System.Linq.Expressions;
using FluentResults;

namespace T8_Repositories_Adapters.source;


public interface IDto
{
    string Id { get;  }
    
}

public interface IStorageAdapter<T> where T : IDto
{
    Task<Result<T>> GetByIdAsync(string id, CancellationToken token);




    Task<Result<IList<T>>> GetAllAsync(CancellationToken token);

    /// <summary>
    /// Save or replace the entity if it already exists
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Result<T>> InsertOrUpdateAsync(T dataObject, CancellationToken token);

    /// <summary>
    /// Delete the entity by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(string id, CancellationToken token);

    IQueryable<T> GetQueryable();

    Task<Result> UpdateOneFieldAsync<TField>(string id,
        Expression<Func<T, TField>> fieldExpression,
        TField updatedValue,
        CancellationToken cancellationToken);
    
    Task<long> EstimatedCountAsync(CancellationToken cancellationToken = default);


}