namespace _2025_xunit_to_the_limits_src;

public class T3_PrepareWithFixture  : IClassFixture<SimpleSyncLifeTime>
{
    
    [Fact]
    public void Test1()
    {
        Assert.True(true);
        // testableValue.Should().Be(42);
    }
    
    // need to have that fixture
    public T3_PrepareWithFixture(SimpleSyncLifeTime fixture)
    {
        Console.WriteLine(fixture.TestableValue);
    }
    
}



public class SimpleSyncLifeTime : IDisposable
{

    public int TestableValue { get; private set; }
    public SimpleSyncLifeTime()
    {
        Console.WriteLine("this is the SETUP");
        TestableValue = 42;
    }
    
    
    public void Dispose()
    {
        TestableValue = 0;
        Console.WriteLine("this is the TEARDOW");
    }
}