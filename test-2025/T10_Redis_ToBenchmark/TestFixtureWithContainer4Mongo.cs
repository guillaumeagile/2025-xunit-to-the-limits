using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Testcontainers.Redis;

namespace _2025_xunit_to_the_limits_src.T10_Redis_ToBenchmark;

public class TestFixtureWithContainer4Redis  : IAsyncLifetime    // <----- ⚠️ 
{
    public RedisContainer RedisContainer { get; private set; }

    public ILogger TestLogger { get; set; } = NullLogger.Instance;

    public async Task InitializeAsync()
    {
        RedisContainer = new RedisBuilder()
            .WithImage("redis:7.0")
            .Build();
        await RedisContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
         await RedisContainer.DisposeAsync();
    }

  
}