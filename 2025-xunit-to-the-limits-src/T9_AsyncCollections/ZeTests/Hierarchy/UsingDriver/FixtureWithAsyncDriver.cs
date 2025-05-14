using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T9_AsyncCollections.TheFriends;
using Microsoft.Extensions.Logging;
using NUlid;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections.ZeTests.Hierarchy.UsingDriver;


[Collection(nameof(FixtureWithAsyncDriver))]
public class SocialAsyncTestWithDriver : SocialAsyncTestBase, IClassFixture<FixtureWithAsyncDriver>
{
    public SocialAsyncTestWithDriver(FixtureWithAsyncDriver fixture, ITestOutputHelper outputHelper) : base(fixture,
        outputHelper)
    {
    }
}

[Collection(nameof(FixtureWithAsyncDriver))]
public class SecondSocialAsyncTestWithDriver : SocialAsyncTestBase, IClassFixture<FixtureWithAsyncDriver>
{
    public SecondSocialAsyncTestWithDriver(FixtureWithAsyncDriver fixture, ITestOutputHelper outputHelper) : base(fixture,
        outputHelper)
    {
    }
}


public class FixtureWithAsyncDriver : TestFixtureWithAnyAsyncRepo
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