using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.TheFriends;
using Microsoft.Extensions.Logging;
using NUlid;

namespace _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.ZeTests.Hierarchy.UsingSameDriver;

public class ZeFixtureWithAsyncDriver : TestFixtureWithAnyAsyncRepo
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        TestLogger.LogInformation(
            "SocialAsyncTestWithDriver InitializeAsync NEVER LOGGED"); //LOGGER IS NOT AVAILABLE AT THIS POINT
        Repository = new AsyncDriverRepository<Element>();
        Uid = Ulid.NewUlid().ToString();
        //the driver has an awaitable method to boot up
        await Task.Delay(500);
    }
}