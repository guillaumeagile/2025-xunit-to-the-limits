using DotNet.Testcontainers.Images;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Playwright;
using Testcontainers.Redis;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

[CollectionDefinition(nameof(SharedPlaywrightCollectionAndRedisContainers))]
public class SharedPlaywrightCollectionAndRedisContainers : ICollectionFixture<PlaywrightFixtureWithRedisContainer> {}

// ReSharper disable once ClassNeverInstantiated.Global
public class PlaywrightFixtureWithRedisContainer : PlaywrightFixture
{
    private const string _redisImage = "redis:7-alpine";
    /*
     redis:7-alpine - ~32MB (smallest, based on Alpine Linux)
     redis:7-slim - ~41MB (Debian-based but stripped down)
     redis:7 - ~138MB (full Debian-based image)
       */
    
    private RedisContainer? _redisContainer;
    public ILogger TestLogger { get; set; } = NullLogger.Instance;
    
    public override async Task InitializeAsync()
    {
        var builder = new RedisBuilder()
            .WithImage(_redisImage)
            .WithCleanUp(true)
            .WithPortBinding(6379, true) 
            .WithImagePullPolicy(PullPolicy.Missing)
            .WithLogger(TestLogger);
       
        _redisContainer = builder.Build();
      
        await _redisContainer.StartAsync();
        TestLogger.LogInformation("RedisContainer started");
        
        await base.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        if (_redisContainer != null)
            await _redisContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    public string RedisConnectionString()
    {
        return _redisContainer?.GetConnectionString() ?? throw new InvalidOperationException("Redis container not started");
    }
}
