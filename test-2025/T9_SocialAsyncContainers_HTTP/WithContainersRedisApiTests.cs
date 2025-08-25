using System.Text.Json;
using AwesomeAssertions;
using Microsoft.Playwright;
using T8_Repositories_Adapters.source;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

// Redis-based API tests with TestContainers
// REST API with the WebAppFactory and Redis container for storage

[Collection(nameof(SharedPlaywrightCollectionAndRedisContainers))]
public class WithContainersRedisApiTests : IClassFixture<PlaywrightFixtureWithRedisContainer>, IAsyncLifetime
{
    private readonly IPlaywright _playwright;
    private WafWithRedisAdapter _waf;
    private readonly PlaywrightFixtureWithRedisContainer _fixture;

    public WithContainersRedisApiTests(PlaywrightFixtureWithRedisContainer fixture, ITestOutputHelper outputHelper)  
    {
        _playwright = fixture.PlaywrightInstance;
        fixture.TestLogger = outputHelper.ToLogger<WithContainersRedisApiTests>();
        _fixture = fixture;
    }

    public Task InitializeAsync()
    {
        var redisConnectionString = _fixture.RedisConnectionString();
        _waf = new WafWithRedisAdapter(redisConnectionString);  
        // And voila, the WAF is connected to the Redis container!
        
        _waf.UseKestrel(cfg => { cfg.ListenLocalhost(1235); }); // Different port from MongoDB tests
        _waf.StartServer(); //  no StartAsync yet :(
        
        //BENEFIT: No infrastructure required to launch the API and its Redis database :)
        
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task CallRouteGetById_WithRedisStorage()
    {
        //ARRANGE the data in the Redis storage
        IStorageAdapter<SomeDto> storageAdapter = _waf.RedisStorageAdapter;
        storageAdapter.Should().NotBeNull();
        var someDto = new SomeDto("redis-test-22", "Redis Foobar", 42);   
        var resultInsertOrUpdate = await storageAdapter.InsertOrUpdateAsync(someDto, CancellationToken.None);
        resultInsertOrUpdate.IsSuccess.Should().BeTrue("Redis insertion should succeed");
        
        //ARRANGE the http call
        var weatherPath = _waf.ClientOptions.BaseAddress.ToString() + "stored/redis-test-22";
        await using var ctx = await _playwright.APIRequest.NewContextAsync();
        
        //ACT
        await using var response = await ctx.GetAsync(weatherPath);
    
        // ASSERT
        response.Ok.Should().BeTrue("API should return success for existing Redis item");
        var json = ((await response.JsonAsync())!)
            .Value
            .Should().BeOfType<JsonElement>()
            .Subject;
        // fast compare: check if the json can be deserialized to the same object
        var deserializedDto = JsonSerializer.Deserialize<SomeDto>(json.ToString());
        deserializedDto.Should().BeEquivalentTo(someDto, "Retrieved data should match stored data in Redis");
    }

    [Fact]
    public async Task CallRouteGetById_NonExistentItem_ShouldReturn404()
    {
        //ARRANGE the http call for non-existent item
        var weatherPath = _waf.ClientOptions.BaseAddress.ToString() + "stored/non-existent-redis-item";
        await using var ctx = await _playwright.APIRequest.NewContextAsync();
        
        //ACT
        await using var response = await ctx.GetAsync(weatherPath);
    
        // ASSERT
        response.Status.Should().Be(404, "Non-existent items should return 404 from Redis storage");
    }

    [Fact]
    public async Task CallRouteGetById_MultipleItems_WithRedisStorage()
    {
        //ARRANGE multiple items in Redis storage
        IStorageAdapter<SomeDto> storageAdapter = _waf.RedisStorageAdapter;
        
        var item1 = new SomeDto("redis-multi-1", "Redis Item One", 100);
        var item2 = new SomeDto("redis-multi-2", "Redis Item Two", 200);
        
        await storageAdapter.InsertOrUpdateAsync(item1, CancellationToken.None);
        await storageAdapter.InsertOrUpdateAsync(item2, CancellationToken.None);
        
        await using var ctx = await _playwright.APIRequest.NewContextAsync();
        
        //ACT & ASSERT for first item
        var path1 = _waf.ClientOptions.BaseAddress.ToString() + "stored/redis-multi-1";
        await using var response1 = await ctx.GetAsync(path1);
        response1.Ok.Should().BeTrue();
        var json1 = await response1.JsonAsync();
        var deserializedDto1 = JsonSerializer.Deserialize<SomeDto>(json1.ToString());
        deserializedDto1.Should().BeEquivalentTo(item1);
        
        //ACT & ASSERT for second item
        var path2 = _waf.ClientOptions.BaseAddress.ToString() + "stored/redis-multi-2";
        await using var response2 = await ctx.GetAsync(path2);
        response2.Ok.Should().BeTrue();
        var json2 = await response2.JsonAsync();
        var deserializedDto2 = JsonSerializer.Deserialize<SomeDto>(json2.ToString());
        deserializedDto2.Should().BeEquivalentTo(item2);
    }

    public Task DisposeAsync()
    {
        _waf?.Dispose();
        return Task.CompletedTask;
    }
}
