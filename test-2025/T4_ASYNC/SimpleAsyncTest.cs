using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T4_ASYNC;

public class SimpleAsyncTest : IClassFixture<AsyncLifeTimeWithLoggerFixture>, IAsyncLifetime
{
    private readonly string _filePath;
    private readonly AsyncLifeTimeWithLoggerFixture _fixture;


    public SimpleAsyncTest(AsyncLifeTimeWithLoggerFixture fixture, ITestOutputHelper outputHelper)
    {
        fixture.SetOutputToLogger(outputHelper); // ok for later,
                                                 // but too late for ctor and initAsync of the fixture
        _fixture = fixture;
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "test-2025.dll");
        fixture.TestLogger.LogInformation("SimpleAsyncTest  constructed");
    }
    
    [Fact]
    public void ExecuteSync_sync()
    {
        var sut = new SutClassAsync(_fixture.TestLogger);
        _fixture.TestLogger.LogInformation("begin Execute sync test ");
        sut.SyncCompute(_filePath);
        _fixture.TestLogger.LogInformation("finished Execute sync test");
    }


    [Fact]
    public void ExecuteSync_NotAwaitAsync()
    {
        var sut = new SutClassAsync(_fixture.TestLogger);
        _fixture.TestLogger.LogInformation("begin ExecuteAsync test");
        sut.ASyncCompute(_filePath);
        _fixture.TestLogger.LogInformation("💥💥💥  finished ExecuteAsync test");
    }


    [Fact]
    public async Task ExecuteAsync_OK()
    {
        var sut = new SutClassAsync(_fixture.TestLogger);
        _fixture.TestLogger.LogInformation("begin ExecuteAsync test");

        await sut.ASyncCompute(_filePath);
        _fixture.TestLogger.LogInformation("finished ExecuteAsync test");
    }

    [Fact]
    public async Task Rewrite_ExecuteSync_Await_AndVerifyResult()
    {
        var sut = new SutClassAsync(_fixture.TestLogger);

        var actual = await sut.ASyncCompute(_filePath);

        actual.Should().BeAssignableTo<String>().And
            .NotBeAssignableTo<Task>();
    }

    public Task InitializeAsync()
    {
        _fixture.TestLogger.LogInformation("🎬🎬🎬🎬 InitializeAsync");
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _fixture.TestLogger.LogInformation("✋✋✋✋ DisposeAsync");
        return Task.CompletedTask;
    }
}