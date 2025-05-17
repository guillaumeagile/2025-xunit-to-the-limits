using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections.TheFriends;

public class AsyncFakeRepository<T> : IAsyncRepository<Element>
{
    public async Task<bool> SaveAsync(Element anElement)
    {
        await Task.Delay(500);
        return true;
    }
}