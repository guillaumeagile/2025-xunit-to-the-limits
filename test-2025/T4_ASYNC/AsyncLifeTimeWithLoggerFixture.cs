using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T4_ASYNC;

public class AsyncLifeTimeWithLoggerFixture : IAsyncLifetime, IDisposable
{
    public ILogger TestLogger { get; private set; } = NullLogger.Instance;

    public int TestableValue { get; private set; }

    public void SetOutputToLogger(ITestOutputHelper outputHelper) =>
        TestLogger = outputHelper.ToLogger<AsyncLifeTimeWithLoggerFixture>();
    
    public AsyncLifeTimeWithLoggerFixture()
    {
        TestLogger.LogWarning("you cannot log into the ctor of the fixture"); // WARNING ! you will never see this
        TestableValue = 42;
    }
  

    public Task InitializeAsync()
    {
        TestLogger.LogCritical("👓 👓 👓  InitializeAsync");
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        TestLogger.LogCritical("👓 👓 👓  DisposeAsync");
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        TestableValue = 0;
        TestLogger.LogCritical("👓 👓 👓  could be the TEARDOW of the fixture");
    }
}