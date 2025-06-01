using NUlid;

namespace _2025_xunit_to_the_limits_src.T3_Fixtures;

public class SimpleSyncLifeTimeFixture : IDisposable
{
    public int TestableValue { get; set; }

    public string Uid { get; init; }

    public SimpleSyncLifeTimeFixture()
    {
        Console.WriteLine("this is the SETUP");
        TestableValue = 42;
        Uid = Ulid.NewUlid().ToString();
    }


    public void Dispose()
    {
        TestableValue = 0;
        Console.WriteLine("this is the TEARDOW");
    }
}