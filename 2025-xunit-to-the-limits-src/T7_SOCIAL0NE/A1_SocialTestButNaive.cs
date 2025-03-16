using FluentAssertions;
using NUlid;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

public class SocialTest
{
    [Fact]
    public void SolitaryTest()
    {
        var anElement = new Element();
        
        var sut = new MyService  ();

        var resOfSave =   sut.SaveAlone(anElement);
        
        resOfSave.Should().BeTrue();
    }
    
    
    [Fact]
    public void MoreSocialTest()
    {
        var anElement = new Element();
        var fakeRepository = new BrokenFakeRepository<Element>();
        
        var sut = new MyService  (fakeRepository);

        var resOfSave =   sut.SaveSocial(anElement);
        
        resOfSave.Should().BeFalse();
    }
    
}