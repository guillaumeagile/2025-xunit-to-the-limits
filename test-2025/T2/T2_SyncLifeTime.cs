using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.T2;

public class T2_SyncLifeTime
{
    private int field = 0;

    public T2_SyncLifeTime()
    {
        
    }
    
    public void Setup()
    {
        field = 2;
    }

    [Fact]
    public void SyncLifeTimeTest()
    {
        field.Should().Be(2);
        field = 3;
    }
    
    [Fact]
    public void SyncLifeTimeTestAgain()
    {
        field.Should().Be(3);
    }


    //  [SetUp]  //there is no such thing


    // ctor
}