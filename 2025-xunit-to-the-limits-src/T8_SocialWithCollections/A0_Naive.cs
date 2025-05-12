using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T8_SocialWithCollections;

[Collection(nameof(TestFixtureWithFake))]
public class A0_SocialTestCollectable : IClassFixture<TestFixtureWithAnyRepo>
{
    private readonly MyService sut;

    public A0_SocialTestCollectable(TestFixtureWithFake fixture)
    {
        sut = new MyService(fixture.Repository);
    }


    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeFalse();
    }
}