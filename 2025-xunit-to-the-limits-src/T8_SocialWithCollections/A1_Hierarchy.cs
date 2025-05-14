using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T8_SocialWithCollections;

public abstract class SocialTestBase : IClassFixture<TestFixtureWithAnyRepo>
{
    private readonly MyService _sut;

    protected SocialTestBase(TestFixtureWithAnyRepo fixture)
    {
        _sut = new MyService(fixture.Repository);
    }


    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = _sut.SaveSocial(anElement);

        resOfSave.Should().BeTrue("this tests is supposed to run with a friend, not a foe!");
    }
}

[Collection(nameof(TestFixtureWithFake))]
public class SocialTestWithFake : SocialTestBase, IClassFixture<TestFixtureWithFake>
{
    public SocialTestWithFake(TestFixtureWithFake fixture) : base(fixture)
    {
    }
}

[Collection(nameof(TestFixtureWithDriver))]
public class SocialTestWithDriver : SocialTestBase, IClassFixture<TestFixtureWithDriver>
{
    public SocialTestWithDriver(TestFixtureWithDriver fixture) : base(fixture)
    {
    }
}