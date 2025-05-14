using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public class AsyncDriverRepository<T> : IAsyncRepository<Element>
{
    public Task<bool> SaveAsync(Element anElement)
    {
        return Task.FromResult(false);
    }
}