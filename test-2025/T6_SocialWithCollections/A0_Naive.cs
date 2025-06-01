using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections;

[Collection(nameof(CollectionOfTestsWithFake))] //we force to use the fake
                                                                            // [Collection(nameof(CollectionOfTestsWithDriver))]   // now try with the driver
public class A0_SocialTestCollectable : IClassFixture<TestFixtureWithAnyRepo>
{
    private readonly MyService sut;

    public A0_SocialTestCollectable(TestFixtureWithFake fixture)
    {
        sut = new MyService(fixture.Repository);
    }
//   public A0_SocialTestCollectable(TestFixtureWithRepo fixture)
    

    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeFalse("because i'm using fake");
    }
}