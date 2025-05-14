using System.Security.Cryptography;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T5_ASYNC;

public class SimpleAsyncTest_NotOK : IClassFixture<SimpleSyncLifeTimeWithLoggerFixture>
{
    private readonly string _filePath;
    private readonly SimpleSyncLifeTimeWithLoggerFixture _fixture;


    public SimpleAsyncTest_NotOK(SimpleSyncLifeTimeWithLoggerFixture fixture, ITestOutputHelper outputHelper)
    {
        fixture.SetOutputToLogger(outputHelper);
        _fixture = fixture;
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "2025-xunit-to-the-limits-src.dll");
        fixture.TestLogger.LogInformation("SimpleAsyncTest_NotOK constructed");
    }


    [Fact]
    public void ExecuteSync_sync()
    {
        var sut = new AsyncClass(_fixture.TestLogger);
        _fixture.TestLogger.LogInformation("begin Execute sync test ");
        sut.SyncCompute(_filePath);
        _fixture.TestLogger.LogInformation("finished Execute sync test");
    }


    [Fact]
    public void ExecuteSync_NotAwaitAsync()
    {
        var sut = new AsyncClass(_fixture.TestLogger);
        _fixture.TestLogger.LogInformation("begin ExecuteAsync test");
        sut.ASyncCompute(_filePath);
        _fixture.TestLogger.LogInformation("finished ExecuteAsync test");
    }


    [Fact]
    public async Task ExecuteAsync_OK()
    {
        var sut = new AsyncClass(_fixture.TestLogger);
        _fixture.TestLogger.LogInformation("begin ExecuteAsync test");

        await sut.ASyncCompute(_filePath);
        _fixture.TestLogger.LogInformation("finished ExecuteAsync test");
    }
    
    [Fact]
    public async void Rewrite_ExecuteSync_Await_AndVerifyResult()
    {
        var sut = new AsyncClass(_fixture.TestLogger);
        
        var actual = await sut.ASyncCompute(_filePath);

        actual.Should().BeAssignableTo<String>().And
        .NotBeAssignableTo<Task>();
    }
    
}

public class SimpleSyncLifeTimeWithLoggerFixture : IDisposable
{
    public ILogger TestLogger { get; private set; } = NullLogger.Instance;

    public int TestableValue { get; private set; }

    public void SetOutputToLogger(ITestOutputHelper outputHelper) =>
        TestLogger = outputHelper.ToLogger<SimpleSyncLifeTimeWithLoggerFixture>();


    public SimpleSyncLifeTimeWithLoggerFixture()
    {
        TestLogger.LogWarning("this is the SETUP");
        TestableValue = 42;
    }


    public void Dispose()
    {
        TestableValue = 0;
        TestLogger.LogTrace("this is the TEARDOW");
    }
}