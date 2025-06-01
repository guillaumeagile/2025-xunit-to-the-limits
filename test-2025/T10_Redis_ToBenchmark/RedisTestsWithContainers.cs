using _2025_xunit_to_the_limits_src.T10_Redis_ToBenchmark.noWay;
using Microsoft.Extensions.Logging;
using T8_Repositories_Adapters.sourceT10;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T10_Redis_ToBenchmark;

[Collection(nameof(TestFixtureWithContainer4Redis))]
public class RedisTestsWithContainers : IClassFixture<TestFixtureWithContainer4Redis>, IAsyncLifetime
{
    private readonly RedisStorageAdapter<TestDto> _adapter;

    public ILogger<RedisTestsWithContainers> TestLogger { get; init; }

    public RedisTestsWithContainers(TestFixtureWithContainer4Redis fixture, ITestOutputHelper outputHelper)
    {
        TestLogger = outputHelper.ToLogger<RedisTestsWithContainers>();
        
        var redisConnectionString = fixture.RedisContainer.GetConnectionString();
        
        var redisConnection = new RedisConnection(redisConnectionString);
        
        _adapter = new RedisStorageAdapter<TestDto>(redisConnection);
    }

    [Fact]
    public async Task InsertAndGetByIdAsync_ShouldWorkCorrectly()
    {
        // Arrange
        var testDto = new TestDto
        {
            Name = "Test Item",
            Value = 42
        };
        // Act
        var insertResult = await _adapter.InsertOrUpdateAsync(testDto, CancellationToken.None);
        var getResult = await _adapter.GetByIdAsync(testDto.Id, CancellationToken.None);

        // Assert
        Assert.True(insertResult.IsSuccess);
        Assert.True(getResult.IsSuccess);
        Assert.Equal(testDto.Id, getResult.Value.Id);
        Assert.Equal(testDto.Name, getResult.Value.Name);
        Assert.Equal(testDto.Value, getResult.Value.Value);
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}