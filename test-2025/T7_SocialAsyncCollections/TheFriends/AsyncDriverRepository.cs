using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.TheFriends;

public class AsyncDriverRepository<T> : IAsyncRepository<Element>
{
    public Task<bool> SaveAsync(Element anElement)
    {
        Thread.Sleep(3000);
        return Task.FromResult(true);
    }
}