using DotNet.Testcontainers.Images;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Playwright;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

[CollectionDefinition(nameof(SharedPlaywrightCollectionAndContainers))]
public class SharedPlaywrightCollectionAndContainers : ICollectionFixture<PlaywrightFixtureWithContainers> {}

// ReSharper disable once ClassNeverInstantiated.Global
public class PlaywrightFixtureWithContainers : PlaywrightFixture
{
    private const string _mongoImage = "mongo:7.0.16-jammy";
    
    private MongoDbContainer? _mongoContainer;
    private MongoClient dbClient;
    private string _dbName;
    public ILogger TestLogger { get; set; } = NullLogger.Instance;
    
    public override async Task InitializeAsync()
    {
        var builder = new MongoDbBuilder()
            .WithImage(_mongoImage)
            .WithCleanUp(true)
            .WithImagePullPolicy(  PullPolicy.Missing)
            .WithLogger(TestLogger );;
       
        _mongoContainer = builder.Build();
      
        await _mongoContainer.StartAsync();
        TestLogger.LogInformation("MongoDbContainer started");
        
        dbClient = new MongoClient(_mongoContainer.GetConnectionString());
        
        dbClient.GetDatabase(NewDbName());  
        await base.InitializeAsync();
    }


    public async Task DisposeAsync()
    {
        await _mongoContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    public string DbConnectionString()
    {
        return _mongoContainer.GetConnectionString();
    }

    public string NewDbName()
    {
        _dbName = NUlid.Ulid.NewUlid().ToString();    //TRICK !!!!!
        return _dbName; 
    }
}