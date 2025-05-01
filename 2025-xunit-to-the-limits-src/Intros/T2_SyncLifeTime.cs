using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.Intros;

public class T2_SyncLifeTime
{
    private int field = 0;
  
    public void Setup()
    {
        field = 2;
    }
    
    [Fact]
    public void SyncLifeTimeTest()
    {
        field.Should().Be(2);
    }
    
    
    
      
  //  [SetUp]  //there is no such thing
 
  
    
    
    
    
    
      // ctor
}