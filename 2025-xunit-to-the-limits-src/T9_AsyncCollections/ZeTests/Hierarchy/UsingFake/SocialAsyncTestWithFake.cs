using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T9_AsyncCollections.TheFriends;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections.ZeTests.Hierarchy.UsingFake;


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

