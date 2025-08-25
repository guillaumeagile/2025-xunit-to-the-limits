namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections.WithInheritance;

[Collection(nameof(TestFixtureWithDriver))]
public class SocialTestWithDriver : SocialTestBase, IClassFixture<TestFixtureWithDriver>
{
    public SocialTestWithDriver(TestFixtureWithDriver fixture) : base(fixture)
    {
    }
}

[Collection(nameof(TestFixtureWithFake))]
public class SocialTestWithFake : SocialTestBase, IClassFixture<TestFixtureWithFake>
{
    public SocialTestWithFake(TestFixtureWithFake fixture) : base(fixture)
    {
    }
}