using Microsoft.Extensions.Logging;

namespace _2025_xunit_to_the_limits_src.T3_Fixtures;

public class T3_SutClass
{
    public T3_SutClass(ILogger anyLogger)
    {
        anyLogger?.LogInformation("HEEYYYYY !!!! logger SHOULD BE available in SutClass");
        anyLogger?.LogDebug("TestableValue of Sut = " + TestableValue.ToString());
    }

    public int TestableValue { get; set; } = 88;
}