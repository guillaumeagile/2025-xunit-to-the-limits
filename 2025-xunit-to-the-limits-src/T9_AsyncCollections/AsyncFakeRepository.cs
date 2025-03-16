using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public class AsyncFakeRepository<T> : IAsyncRepository<Element>
{
    public Task<bool> SaveAsync(Element anElement)
    {
        throw new NotImplementedException("AsyncFakeRepository");
    }
}