using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.Fixtures;
using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using AwesomeAssertions;
using TestFixtureWithFake = _2025_xunit_to_the_limits_src.T6_SocialWithCollections.TestFixtureWithFake;

namespace _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;


/*

what if, the friend is a foe, coming from the fixture ?

*/
public class A3_SocialTestBoundToOneFixture : IClassFixture<TestFixtureWithFake>
{
    private readonly MyService sut;

    public A3_SocialTestBoundToOneFixture(TestFixtureWithFake fixture)
    {
        sut = new MyService(fixture.Repository);
    }


    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeTrue("are you a friend ?");  
        // hey, it"s a different behaviour.... neeed to change this assertion !!!
    }
}