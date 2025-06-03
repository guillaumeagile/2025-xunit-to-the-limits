using System.Text.Json;
using FluentAssertions;
using Microsoft.Playwright;
using T8_Repositories_Adapters.source;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

[Collection(nameof(SharedPlaywrightCollectionAndContainers))]
public class WithContainersApiTests : IClassFixture<PlaywrightFixtureWithContainers>, IAsyncLifetime
{
    private readonly IPlaywright _playwright;
    private WafWithMongoAdapter _waf;
    private readonly PlaywrightFixtureWithContainers _fixture;

    public WithContainersApiTests(PlaywrightFixtureWithContainers fixture, ITestOutputHelper outputHelper)  
    {
        _playwright = fixture.PlaywrightInstance;
        fixture.TestLogger = outputHelper.ToLogger<WithContainersApiTests>();
        
        // je dois faire le register service de l'adapter avec la connexion string issue du container 
        // obligé de le faire dans InitializeAsync car sinon c'est trop tôt
        
        _fixture = fixture;
    }

    public Task InitializeAsync()
    {
        var mongoDbConnection = new MongoDbConnection(_fixture.DbConnectionString(), _fixture.NewDbName());
        _waf = new WafWithMongoAdapter(mongoDbConnection);  
        // et voila, le WAF est connecté au container !
        
        _waf.UseKestrel(cfg => { cfg.ListenLocalhost(1234); });
        _waf.StartServer(); // toujours pas de StartAsync :(
        
        //BENEFICE: aucune infrastructure requise pour lancer l'API et sa base de données :)
        
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task CallRouteGetById()
    {
        //ARRANGE the data in the storage
        IStorageAdapter<SomeDto> storageAdapter = _waf.MongoStorageAdapter;
        storageAdapter.Should().NotBeNull();
        var someDto = new SomeDto("2", "Foobar", 42);   //try with empty string as ID
        var resultInsertOrUpdate = await storageAdapter.InsertOrUpdateAsync(someDto, CancellationToken.None);
        //resultInsertOrUpdate.IsSuccess.Should().BeTrue();
        
        //ARRANGE the http call
        var weatherPath = _waf.ClientOptions.BaseAddress.ToString() + "stored/2"  ;
        await using var ctx = await _playwright.APIRequest.NewContextAsync();
        
        //ACT
        await using var response = await ctx.GetAsync(weatherPath);
    
        // ASSERT
        response.Ok.Should().BeTrue();
        var json = ((await response.JsonAsync())!)
            .Value
            .Should().BeOfType<JsonElement>()
            .Subject;
        // fast compare: check if the json can be deserialized to the same object
        var deserializedDto = JsonSerializer.Deserialize<SomeDto>(json.ToString());
        deserializedDto.Should().BeEquivalentTo(someDto);
        
    }

    

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}