using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public class AsyncFakeRepository<T> : IAsyncRepository<Element>
{
    public async Task<bool> SaveAsync(Element anElement)
    {
        await Task.Delay(500);
        return true;
    }
}