using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.ZeTests.Hierarchy.UsingSameDriver;

[Collection(nameof(ZeFixtureWithAsyncDriver))]
public class SecondSocialAsyncTestWithDriver : SocialAsyncTestBase, IClassFixture<ZeFixtureWithAsyncDriver>
{
    public SecondSocialAsyncTestWithDriver(ZeFixtureWithAsyncDriver zeFixture, ITestOutputHelper outputHelper) : base(zeFixture,
        outputHelper)
    {
    }
}