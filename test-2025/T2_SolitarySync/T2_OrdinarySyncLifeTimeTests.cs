using AwesomeAssertions;

namespace _2025_xunit_to_the_limits_src.T2;

public class T2_OrdinarySyncLifeTimeTests
{
    private T2_SystemUnderTest _sut = new();

    public T2_OrdinarySyncLifeTimeTests()
    {
    }

    public void Setup()
    {
        _sut.Field = 2;
    }

    [Fact]
    public void SyncLifeTimeTest()
    {
        _sut.Field.Should().Be(2);
        _sut.Field = 3;
    }

    [Fact]
    public void SyncLifeTimeTestAgain()
    {
        _sut.Field.Should().Be(3);
    }


    //  [SetUp]  //there is no such thing


    // ctor
}