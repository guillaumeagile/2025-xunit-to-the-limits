using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

[Collection(nameof(CollectionOfTestsWithFake))]
public class A2_SocialTestCollectableBoundToOneFixture : IClassFixture<TestFixtureWithFake>
{
    private readonly MyService sut;

    public A2_SocialTestCollectableBoundToOneFixture(TestFixtureWithFake fixture)
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