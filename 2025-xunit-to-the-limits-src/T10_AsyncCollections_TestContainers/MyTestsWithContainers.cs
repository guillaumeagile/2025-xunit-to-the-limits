using _2025_xunit_to_the_limits_src.T10_AsyncCollections_TestContainers.source;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T9_AsyncCollections;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T10_AsyncCollections_TestContainers;


[Collection(nameof(TestFixtureWithContainer4Mongo))]
public class MyTestsWithContainers : IClassFixture<TestFixtureWithContainer4Mongo>  //, IAsyncLifetime
{
    private readonly string? _mongoConnectionString;
    private readonly TestFixtureWithContainer4Mongo _mongoFixture;
    private MongoDbConnection _mongoDbConnection;
    public ILogger TestLogger { get; init; }
    
    public MyTestsWithContainers(TestFixtureWithContainer4Mongo fixture, ITestOutputHelper outputHelper) 
    {
        TestLogger = outputHelper.ToLogger<MyTestsWithContainers>();
        fixture.TestLogger.LogInformation("TestsWithContainers constructed");
        
        _mongoConnectionString = fixture.DbConnectionString();  // NOOOOOOO ! why ?
        
        _mongoFixture = fixture;
    }

    [Fact]
    public async Task IsolatedProof_MongoDB()
    {
        var mongoAdapter = new MongoAdapter<SomeDto>(_mongoDbConnection);

        var token = new CancellationToken();

        var resultGetAll = await mongoAdapter.GetAllAsync(token);
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().BeEmpty();
    }
    
    [Fact]
    public async Task SaveSomeDto_ToMongoDB()
    {
        var mongoAdapter = new MongoAdapter<SomeDto>(_mongoDbConnection);

        var token = new CancellationToken();
        
        var resultInsertOrUpdate = await mongoAdapter.InsertOrUpdateAsync(
            new SomeDto("1", "Foo", 20), token);
        resultInsertOrUpdate.IsSuccess.Should().BeTrue();

        var resultGetAll = await mongoAdapter.GetAllAsync(token);
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().NotBeEmpty();
        
        var estimatedCount = await mongoAdapter.EstimatedCountAsync(token);
        estimatedCount.Should().Be(1);
        
    }
    
    [Fact]
    public async Task ZIsolatedProof_MongoDB()
    {
        
        var mongoAdapter = new MongoAdapter<SomeDto>(_mongoDbConnection);

        var token = new CancellationToken();

        var resultGetAll = await mongoAdapter.GetAllAsync(token);
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().BeEmpty();
    }

    public Task InitializeAsync()
    {
        TestLogger.LogInformation("TestsWithContainers InitializeAsync");
         _mongoDbConnection = new MongoDbConnection(_mongoFixture.DbConnectionString(), _mongoFixture.DbName());
         TestLogger.LogInformation("MongoDbConnection dbName = " + _mongoFixture.DbName());
        return Task.CompletedTask; 
    }

    public Task DisposeAsync()
    {
     return Task.CompletedTask; 
    }
}

public record SomeDto(string Id, string Name, int Age) : IDto
{
}