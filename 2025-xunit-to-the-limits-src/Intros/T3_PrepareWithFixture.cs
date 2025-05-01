namespace _2025_xunit_to_the_limits_src.Intros;

public class T3_PrepareWithFixture  : IClassFixture<SimpleSyncLifeTimeFixture>
{
    
    public T3_PrepareWithFixture(SimpleSyncLifeTimeFixture fixture)
    {
        Console.WriteLine("entering ctor of " + nameof(T3_PrepareWithFixture));   
        // Console.WriteLine(fixture.TestableValue);
    }
    
    [Fact]
    public void Test1()
    {
        Assert.True(true);
        // testableValue.Should().Be(42);
    }
    
    // need to have that fixture

    
}



public class SimpleSyncLifeTimeFixture : IDisposable
{

    public int TestableValue { get; private set; }
    public SimpleSyncLifeTimeFixture()
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