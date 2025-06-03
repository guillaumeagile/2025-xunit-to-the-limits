using FluentAssertions;
using Microsoft.Extensions.Logging;
using T8_Repositories_Adapters.source;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T8_AsyncCollections_TestContainers;

// TRY TO RUN without the collection, it will create 2 containers at the same time (because of ZOhterTestsWithContainers)
//[Collection(nameof(TestFixtureWithContainer4Mongo))]   // in sequence
//[Collection((nameof(CollectionDefinitionOfTestsWithSameContainer)))] // in parallel
public class FirstMe_TestsWithContainers : IClassFixture<TestFixtureWithContainer4Mongo>, IAsyncLifetime
{
    private readonly TestFixtureWithContainer4Mongo _mongoFixture;
    private MongoDbConnection _mongoDbConnection;
    public ILogger TestLogger { get; init; }

    public FirstMe_TestsWithContainers(TestFixtureWithContainer4Mongo fixture, ITestOutputHelper outputHelper)
    {
        TestLogger = outputHelper.ToLogger<MyTestsWithContainers>();
        fixture.TestLogger.LogInformation("TestsWithContainers constructed");
        _mongoFixture = fixture;
    }
    
    [Fact]
    public async Task SaveFewMoreDto_ToMongoDB()
    {
        var mongoAdapter = new MongoStorageAdapter<SomeDto>(_mongoDbConnection);

       _ = await mongoAdapter.InsertOrUpdateAsync(
            new SomeDto("11", "Foo", 20), CancellationToken.None);

        _ = await mongoAdapter.InsertOrUpdateAsync(
            new SomeDto("111", "Foo", 20), CancellationToken.None);

        _  = await mongoAdapter.InsertOrUpdateAsync(
            new SomeDto("1111", "Foo", 20), CancellationToken.None);
        
        

        var estimatedCount = await mongoAdapter.EstimatedCountAsync(CancellationToken.None);
        estimatedCount.Should().Be(3);
    }
    

    [Fact]
    public async Task ZIsolatedProof_MongoDB()
    {
        var mongoAdapter = new MongoStorageAdapter<SomeDto>(_mongoDbConnection);

        var token = new CancellationToken();

        var resultGetAll = await mongoAdapter.GetAllAsync(token);
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().BeEmpty();
        
        var estimatedCount = await mongoAdapter.EstimatedCountAsync(token);
        estimatedCount.Should().Be(0);
    }


    public Task InitializeAsync()
    {
        TestLogger.LogInformation("TestsWithContainers InitializeAsync");
        _mongoDbConnection = new MongoDbConnection(_mongoFixture.DbConnectionString(), _mongoFixture.NewDbName());
        TestLogger.LogInformation("MongoDbConnection dbName = " + _mongoFixture.DbName());
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
