namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public interface IAsyncRepository<T>
{
    Task<bool> SaveAsync(T anElement);
}