using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T8_SocialWithCollections;
using FluentAssertions;
using NUlid;
using MyService = _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources.MyService;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

public class SocialTest
{
    [Fact]
    public void SolitaryTest()
    {
        var anElement = new Element();

        var sut = new MyService();

        var resOfSave = sut.SaveAlone(anElement);

        resOfSave.Should().BeTrue();
    }


    [Fact]
    public void FirstSocialTest()
    {
        var anElement = new Element();
        var fakeRepository = new DriverRepository<Element>();

        var sut = new MyService(fakeRepository);

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeTrue();
    }
    
    [Fact]
    public void AnotherSocialTest()
    {
        var anElement = new Element();
        var fakeRepository = new BrokenFakeRepository<Element>();

        var sut = new MyService(fakeRepository);

        var resOfSave = sut.SaveSocial(anElement);

        resOfSave.Should().BeFalse();
    }
}