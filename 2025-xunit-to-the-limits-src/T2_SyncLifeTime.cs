using FluentAssertions;

namespace _2025_xunit_to_the_limits_src;

public class T2_SyncLifeTime
{
    
    private int field = 0;
    
  //  [SetUp]  //there is no such thing
  
  // ctor
  
  
    public void Setup()
    {
        field = 2;
    }
    
    [Fact]
    public void SyncLifeTimeTest()
    {

        field.Should().Be(2);
    }
    
    
}