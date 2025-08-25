using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using AwesomeAssertions;

namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections.WithInheritance;

[Trait("Category", "BaseClass")]
public abstract class SocialTestBase : IClassFixture<TestFixtureWithAnyRepo>
{
    private readonly MyService _sut;

    protected SocialTestBase(TestFixtureWithAnyRepo fixture)
    {
        _sut = new MyService(fixture.Repository);
    }


    [Fact]
    public void OneSocialSharedTest()  // launch this test will run both fixtures
    {
        var anElement = new Element();

        var resOfSave = _sut.SaveSocial(anElement);

        resOfSave.Should().BeTrue("this tests is supposed to run with a friend, not a foe!");
    }
}