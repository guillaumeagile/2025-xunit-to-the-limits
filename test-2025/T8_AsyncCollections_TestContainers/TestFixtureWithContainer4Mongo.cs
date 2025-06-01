using DotNet.Testcontainers.Images;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace _2025_xunit_to_the_limits_src.T8_AsyncCollections_TestContainers;

public class TestFixtureWithContainer4Mongo  : IAsyncLifetime    // <----- ⚠️ 
{
    private const string _mongoImage = "mongo:7.0.16-jammy";
    private const int _mongoInternalPort = 27017;
    //  private const int DefaultMongoExternalPort = 27019;
    
    private MongoDbContainer? _mongoContainer;
    private MongoClient dbClient;
    private string _dbName;
    public ILogger TestLogger { get; set; } = NullLogger.Instance;

    public async Task InitializeAsync()
    {
        var builder = new MongoDbBuilder()
            .WithImage(_mongoImage)
         
            .WithCleanUp(true)
            //.WithReuse(true)    // BE CAREFUL !!!! NO MORE ISOLATION
        //    .WithPortBinding(_mongoInternalPort, true)  // ALL 3 together to avoid port conflicts and stall         
            
            .WithImagePullPolicy(  PullPolicy.Missing)
            .WithLogger(TestLogger );;
       
        _mongoContainer = builder.Build();
      
        await _mongoContainer.StartAsync();
        TestLogger.LogInformation("MongoDbContainer started");
        
        dbClient = new MongoClient(_mongoContainer.GetConnectionString());
      
        
        dbClient.GetDatabase(NewDbName());  
    }

    public async Task DisposeAsync()
    {
         await _mongoContainer.DisposeAsync();
    }

    public string? DbConnectionString()
    {
        return _mongoContainer.GetConnectionString();
    }


    public string NewDbName()
    {   
        _dbName = NUlid.Ulid.NewUlid().ToString();    //TRICK !!!!!
        return _dbName; 
    }
}