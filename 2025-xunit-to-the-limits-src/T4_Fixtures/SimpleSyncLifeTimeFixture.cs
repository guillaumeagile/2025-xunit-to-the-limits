namespace _2025_xunit_to_the_limits_src.T4_Fixtures;

public class SimpleSyncLifeTimeFixture : IDisposable
{
    public int TestableValue { get; set; }
    
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