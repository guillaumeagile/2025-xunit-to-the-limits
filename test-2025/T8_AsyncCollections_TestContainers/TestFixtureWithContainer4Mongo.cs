using DotNet.Testcontainers.Builders;
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
           
            // those 3 together to avoid port conflicts and stall
             .WithReuse(true)    // be careful, super fast but no more isolation -> the data volume is shared
            .WithPortBinding(_mongoInternalPort, false)  //fixed port for the container
             .WithWaitStrategy( waitStrategy: Wait.ForUnixContainer().UntilPortIsAvailable(_mongoInternalPort))
            
            // or this one alone to ensure isolation (but not enough)
           // .WithPortBinding(_mongoInternalPort, true) 
            
        
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
        _dbName = "AlwaysTheSameDatabase";
       // _dbName = NUlid.Ulid.NewUlid().ToString();    //TRICK !!!!! 
        return _dbName; 
    }
    
    public string DbName()
    {   
        return _dbName; 
    }
}