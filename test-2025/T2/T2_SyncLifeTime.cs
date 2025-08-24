using AwesomeAssertions;

namespace _2025_xunit_to_the_limits_src.T2;

public class T2_SyncLifeTime
{
    private int _field = 0;

    public T2_SyncLifeTime()
    {
        
    }
    
    public void Setup()
    {
        _field = 2;
    }

    [Fact]
    public void SyncLifeTimeTest()
    {
        _field.Should().Be(2);
        _field = 3;
    }
    
    [Fact]
    public void SyncLifeTimeTestAgain()
    {
        _field.Should().Be(3);
    }


    //  [SetUp]  //there is no such thing


    // ctor
}