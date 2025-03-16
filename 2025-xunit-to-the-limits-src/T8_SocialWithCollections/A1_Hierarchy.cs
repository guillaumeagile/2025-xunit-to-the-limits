using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T8_SocialWithCollections;


//the runner will not run an abstract class
public abstract class SocialTestBase : IClassFixture<TestFixtureWithAnyRepo>
{
    private readonly MyService sut;

    public SocialTestBase(TestFixtureWithAnyRepo fixture)
    {
        sut = new MyService  (fixture.Repository);
    }
    
    
    [Fact]
    public void SocialPalGivenByFixture()
    {
        var anElement = new Element();
   
        var resOfSave =   sut.SaveSocial(anElement);
        
        resOfSave.Should().BeFalse();
    }
}



[Collection(nameof(TestFixtureWithFake))]
public class SocialTestWithFake : SocialTestBase, IClassFixture<TestFixtureWithFake>
{
    public SocialTestWithFake(TestFixtureWithFake fixture ) : base(fixture)
    {
    }
}


[Collection(nameof(TestFixtureWithDriver))]
public class SocialTestWithDriver : SocialTestBase, IClassFixture<TestFixtureWithDriver>
{
    public SocialTestWithDriver(TestFixtureWithDriver fixture ) : base(fixture)
    {
    }
}
