using Microsoft.Extensions.Logging;

namespace _2025_xunit_to_the_limits_src.T4_Fixtures;

public class T4_SutClass
{
    public T4_SutClass(ILogger anyLogger)
    {
        anyLogger.LogInformation("logger available in SutClass");
        anyLogger.LogDebug("TestableValue of Sut = " + TestableValue.ToString());
    }

    public int TestableValue { get; set; } = 88;
}