using System.Text.Json;
using _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;
using DotNet.Testcontainers.Images;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Playwright;
using MongoDB.Driver;
using T8_Repositories_Adapters.source;
using Testcontainers.MongoDb;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.TestCollections;

public class DslFixture
{
    // client for http calls
    protected IBrowser Browser { get; private set; } = null!;
    protected IPlaywright PlaywrightInstance { get; private set; } = null!;

    // WAF for http server
    protected WafWithMongoAdapter Waf { get; private set; }

    // container for Mongo
    private const string _mongoImage = "mongo:7.0.16-jammy";
    private MongoDbContainer? _mongoContainer;
    private MongoClient dbClient;
    private string _dbName;
    public ILogger TestLogger { get; set; } = NullLogger.Instance;

    public async Task InitializeAsync()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync();

        var builder = new MongoDbBuilder()
            .WithImage(_mongoImage)
            .WithCleanUp(true)
            .WithPortBinding(27017, true)
            .WithImagePullPolicy(PullPolicy.Missing)
            .WithLogger(TestLogger);
        ;

        _mongoContainer = builder.Build();

        await _mongoContainer.StartAsync();
        TestLogger.LogInformation("MongoDbContainer started");

        dbClient = new MongoClient(_mongoContainer.GetConnectionString());
        dbClient.GetDatabase(NewDbName());

        var mongoDbConnection = new MongoDbConnection(_mongoContainer.GetConnectionString(), NewDbName());
        Waf = new WafWithMongoAdapter(mongoDbConnection);
        Waf.UseKestrel(cfg => { cfg.ListenLocalhost(1234); });
        Waf.StartServer(); //  no StartAsync yet :(
    }


    public async Task DisposeAsync()
    {
        
        await Browser.DisposeAsync();
        PlaywrightInstance.Dispose();

        await _mongoContainer.DisposeAsync();
    }


    private string NewDbName()
    {
        _dbName = NUlid.Ulid.NewUlid().ToString(); //TRICK !!!!!
        return _dbName;
    }

    public async Task InsertSome(SomeDto someDto)
    {
        IStorageAdapter<SomeDto> storageAdapter = Waf.MongoStorageAdapter;
        await storageAdapter.InsertOrUpdateAsync(someDto, CancellationToken.None);
    }

    public DslFixtureWithRelativePath SetRelativePathTo(string basePath)
    {
        return new DslFixtureWithRelativePath(basePath, this.Waf, this.PlaywrightInstance);    
    }

    public class DslFixtureWithRelativePath(string basePath, WafWithMongoAdapter waf, IPlaywright playwrightInstance) : IDisposable
    {
        private IAPIResponse _response; //Disposable
        private IAPIRequestContext _ctx; //Disposable

        public async Task<DslFixtureWithRelativePath> GetAllAsync()
        {
            var weatherPath = waf.ClientOptions.BaseAddress.ToString() + "stored/2"  ;
              _ctx = await playwrightInstance.APIRequest.NewContextAsync();
             _response = await _ctx.GetAsync(weatherPath);
            _response.Ok.Should().BeTrue();
            return this;
        }
        
        public async  Task<JsonElement?> ExtractJsonAsync()
        {
            return await _response.JsonAsync();
        }   

        public void Dispose()
        {
            CastAndDispose(_response);
            CastAndDispose(_ctx);

            return;

            static void CastAndDispose(IAsyncDisposable resource)
            {
                if (resource is IDisposable resourceDisposable)
                    resourceDisposable.Dispose();
                else
                    _ = resource.DisposeAsync().AsTask();
            }
        }
    }
}