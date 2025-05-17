using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.TheFriends;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.ZeTests.Hierarchy.UsingFake;


[Collection(nameof(TestFixtureWithAsyncFake))]
public class SocialAsyncTestWithFake : SocialAsyncTestBase, IClassFixture<TestFixtureWithAsyncFake>
{
    public SocialAsyncTestWithFake(TestFixtureWithAsyncFake fixture, ITestOutputHelper outputHelper) : base(fixture,
        outputHelper)
    {
    }
}

public class TestFixtureWithAsyncFake : TestFixtureWithAnyAsyncRepo
{
    public TestFixtureWithAsyncFake()
    {
        //this one is fake 
        Repository = new AsyncFakeRepository<Element>();
    }
}

