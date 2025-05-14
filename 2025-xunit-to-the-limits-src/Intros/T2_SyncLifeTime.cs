using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.Intros;

public class T2_SyncLifeTime
{
    private int field = 0;

    public T2_SyncLifeTime()
    {
           field = 2;
    }
    
    public void Setup()
    {
     
    }

    [Fact]
    public void SyncLifeTimeTest()
    {
        field.Should().Be(2);
    }


    //  [SetUp]  //there is no such thing


    // ctor
}