using FluentAssertions;
using Microsoft.Extensions.Logging;
using T8_Repositories_Adapters.source;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T8_AsyncCollections_TestContainers;

// TRY TO RUN without the collection, it will create 2 containers at the same time (because of ZOhterTestsWithContainers)
[Collection(nameof(TestFixtureWithContainer4Mongo))]   // will run in sequence  >>>>> Container-per-class Strategy
//[Collection((nameof(CollectionDefinitionOfTestsWithSameContainer)))]  //   Container-per-collection strategy >>>>  2 classes with the same collectionDefinition will run in parallel, inside each class methods will go in sequence 
public class MyTestsWithContainers : IClassFixture<TestFixtureWithContainer4Mongo>, IAsyncLifetime
{
    private readonly string? _mongoConnectionString;
    private readonly TestFixtureWithContainer4Mongo _mongoFixture;
    private MongoDbConnection _mongoDbConnection;
    public ILogger TestLogger { get; init; }

    public MyTestsWithContainers(TestFixtureWithContainer4Mongo fixture, ITestOutputHelper outputHelper)
    {
        TestLogger = outputHelper.ToLogger<MyTestsWithContainers>();
        fixture.TestLogger = TestLogger;
        fixture.TestLogger.LogInformation("TestsWithContainers constructed");

        //  _mongoConnectionString = fixture.DbConnectionString();  // NOOOOOOO ! why ?

        _mongoFixture = fixture;
    }

    [Fact]
    public async Task IsolatedProof_MongoDB()
    {
        var mongoAdapter = new MongoStorageAdapter<SomeDto>(_mongoDbConnection);

        var resultGetAll = await mongoAdapter.GetAllAsync(CancellationToken.None);
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SaveSomeDto_ToMongoDB()
    {
        var mongoAdapter = new MongoStorageAdapter<SomeDto>(_mongoDbConnection);

        var resultInsertOrUpdate = await mongoAdapter.InsertOrUpdateAsync(
            new SomeDto("1", "Foo", 20), CancellationToken.None);
        resultInsertOrUpdate.IsSuccess.Should().BeTrue();

        var resultGetAll = await mongoAdapter.GetAllAsync(CancellationToken.None);
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().NotBeEmpty();
        resultGetAll.Value.First().Should().Be(new SomeDto("1", "Foo", 20));

        var estimatedCount = await mongoAdapter.EstimatedCountAsync(CancellationToken.None);
        estimatedCount.Should().Be(1);
    }

    [Fact]
    public async Task ZIsolatedProof_MongoDB()
    {
        var mongoAdapter = new MongoStorageAdapter<SomeDto>(_mongoDbConnection);

        var resultGetAll = await mongoAdapter.GetAllAsync(new CancellationToken());
        resultGetAll.IsSuccess.Should().BeTrue();
        resultGetAll.Value.Should().BeEmpty();
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