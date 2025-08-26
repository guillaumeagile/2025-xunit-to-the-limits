using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.ZeTests.Hierarchy.ZUsingAsyncDriver;

[Collection(nameof(ZeFixtureWithAsyncDriver))]
public class FirstSocialAsyncTestWithDriver : SocialAsyncTestBase, IClassFixture<ZeFixtureWithAsyncDriver>
{
    public FirstSocialAsyncTestWithDriver(ZeFixtureWithAsyncDriver zeFixture, ITestOutputHelper outputHelper) : base(zeFixture,
        outputHelper)
    {
    }
}