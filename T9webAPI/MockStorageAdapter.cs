using System.Linq.Expressions;
using FluentResults;
using T8_Repositories_Adapters.source;

namespace T9webAPI;

public class MockStorageAdapter : IStorageAdapter<SomeDto>
{
    private readonly List<SomeDto> _data = new()
    {
        new SomeDto("1", "John Doe", 30),
        new SomeDto("2", "Jane Smith", 25),
        new SomeDto("3", "Bob Johnson", 35)
    };

    public Task<Result<SomeDto>> GetByIdAsync(string id, CancellationToken token)
    {
        var item = _data.FirstOrDefault(x => x.Id == id);
        return item != null 
            ? Task.FromResult(Result.Ok(item))
            : Task.FromResult(Result.Fail<SomeDto>("Item not found"));
    }

    public Task<Result<IList<SomeDto>>> GetAllAsync(CancellationToken token)
    {
        return Task.FromResult(Result.Ok<IList<SomeDto>>(_data));
    }

    public Task<Result<SomeDto>> InsertOrUpdateAsync(SomeDto dataObject, CancellationToken token)
    {
        var existing = _data.FirstOrDefault(x => x.Id == dataObject.Id);
        if (existing != null)
        {
            _data.Remove(existing);
        }
        _data.Add(dataObject);
        return Task.FromResult(Result.Ok(dataObject));
    }

    public Task<Result> DeleteAsync(string id, CancellationToken token)
    {
        var item = _data.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _data.Remove(item);
            return Task.FromResult(Result.Ok());
        }
        return Task.FromResult(Result.Fail("Item not found"));
    }

    public IQueryable<SomeDto> GetQueryable()
    {
        return _data.AsQueryable();
    }

    public Task<Result> UpdateOneFieldAsync<TField>(string id, Expression<Func<SomeDto, TField>> fieldExpression, TField updatedValue, CancellationToken cancellationToken)
    {
        var item = _data.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return Task.FromResult(Result.Fail("Item not found"));
        }
        
        // For this mock, we'll just return success without actually updating
        // In a real implementation, you'd use reflection or expression compilation
        return Task.FromResult(Result.Ok());
    }

    public Task<long> EstimatedCountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult((long)_data.Count);
    }
}