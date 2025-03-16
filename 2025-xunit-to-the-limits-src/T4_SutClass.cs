using Microsoft.Extensions.Logging;

namespace _2025_xunit_to_the_limits_src;

public class T4_SutClass
{
    public T4_SutClass(ILogger  testLogger)
    {
        testLogger.LogInformation("logger available in SutClass");
        testLogger.LogDebug("TestableValue of Sut = " + TestableValue.ToString());
    }
    public int TestableValue { get; } = 88;
}