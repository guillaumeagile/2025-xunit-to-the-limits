using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T9_AsyncCollections.TheFriends;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections.ZeTests.Hierarchy;


[Collection(nameof(TestFixtureWithAsyncDriver))]
public class SocialAsyncTestWithDriver : SocialAsyncTestBase, IClassFixture<TestFixtureWithAsyncDriver>
{
    public SocialAsyncTestWithDriver(TestFixtureWithAsyncDriver fixture, ITestOutputHelper outputHelper) : base(fixture,
        outputHelper)
    {
    }
}

public class TestFixtureWithAsyncDriver : TestFixtureWithAnyAsyncRepo
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        TestLogger.LogInformation(
            "SocialAsyncTestWithDriver InitializeAsync NEVER LOGGED"); //LOGGER IS NOT AVAILABLE AT THIS POINT
        Repository = new AsyncDriverRepository<Element>();

        //the driver has an awaitable method to boot up
        await Task.Delay(500);
    }
}