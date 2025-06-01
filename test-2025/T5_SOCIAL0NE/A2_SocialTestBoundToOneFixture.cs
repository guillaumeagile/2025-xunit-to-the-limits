using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;


/*

what if, the friend is coming from the fixture ?

*/
public class A2_SocialTestBoundToOneFixture : IClassFixture<TestFixtureWithRepo>
{
    private readonly MyService sut;

    public A2_SocialTestBoundToOneFixture(TestFixtureWithRepo fixture)
    {
        sut = new MyService(fixture.Repository);
    }


    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeTrue();
    }
}