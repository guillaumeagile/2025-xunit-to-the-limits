using System.Text.Json;
using _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.TestCollections;
using _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.Tooling;
using FluentAssertions;
using Microsoft.Playwright;
using T8_Repositories_Adapters.source;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL;

// now : all together !
// REST API with the WebAppFactory and containers for the infrastructure (mongo, redis, rabbitmq, ...)

[Collection(nameof(SharedDslCollection))]
public class WithDslTests : IClassFixture<DslFixture>, IAsyncLifetime
{
    //  private readonly IPlaywright _playwright;
    //  private WafWithMongoAdapter _waf;
    private readonly DslFixture _fixtureDsl;

    public WithDslTests(DslFixture fixtureDsl, ITestOutputHelper outputHelper)
    {
        _fixtureDsl = fixtureDsl;
        fixtureDsl.TestLogger = outputHelper.ToLogger<WithDslTests>();

        // the idea is that the DSL simplifies the setup and the plumbing
        // so that the tests are focused on the business logic
        // also the DSL will expose what is required for the business logic to be tested in a clean way
    }

    public async Task InitializeAsync()
    {
        // var mongoDbConnection = new MongoDbConnection(_fixture.DbConnectionString(), _fixture.NewDbName());
        //  _waf = new WafWithMongoAdapter(mongoDbConnection);  
        //_waf.UseKestrel(cfg => { cfg.ListenLocalhost(1234); });
        //  _waf.StartServer(); //  no StartAsync yet :(
        await this._fixtureDsl.InitializeAsync(); // HERE !!!! super important to AWAIT for this

        //BENEFIT: the plumbing is in the DSL, and easier to share across tests
        // this also the reduce the surface of friction between test plumbing (which was too big)
        // and limit the misusage of all the stuff: playwright+waf+testContainers
        // the DSL rules them all 💪
    }

    [Fact]
    public async Task CallRouteGetById()
    {
        //ARRANGE the data in the storage -> in the DSL
        // IStorageAdapter<SomeDto> storageAdapter = _waf.MongoStorageAdapter;
        // storageAdapter.Should().NotBeNull();

        var someDto = new SomeDto("2", "Foobar", 42); //try with empty string as ID
        // now in the DSL
        //     var resultInsertOrUpdate = await storageAdapter.InsertOrUpdateAsync(someDto, CancellationToken.None);
        // the DSL must tell a story
        await _fixtureDsl.InsertSome(someDto);


        //ARRANGE the http call
        using var dsl1 = await (_fixtureDsl.SetRelativePathTo("stored/2")
            // in the DSL , we hide the technical stuff 
            //        await using var ctx = await _playwright.APIRequest.NewContextAsync()
            // and simply ACT
            //  await using var response = await ctx.GetAsync(weatherPath);
            .GetAllAsync());

        var json = await dsl1.ExtractJsonAsync();
        json.HasValue.Should().BeTrue();

        // fast compare: check if the json can be deserialized to the same object
        var deserializedDto = JsonSerializer.Deserialize<SomeDto>(json.ToString());
        deserializedDto.Should().BeEquivalentTo(someDto);
    }
    
    [Fact]
    public async Task CallRouteGetById_Concise()
    {
        var someDto = new SomeDto("2", "Foobar", 42);
        
        await _fixtureDsl
            .InsertSome(someDto)
            .ContinueWith(async _ =>
            {
                using var result = await _fixtureDsl
                    .SetRelativePathTo("stored/2")
                    .GetAllAsync();
                
                var json = await result.ExtractJsonAsync();
                json.HasValue.Should().BeTrue();
                
                var deserializedDto = JsonSerializer.Deserialize<SomeDto>(json.ToString());
                deserializedDto.Should().BeEquivalentTo(someDto);
            });
    }
    


    public async Task DisposeAsync()
    {
        await _fixtureDsl.DisposeAsync();
    }
}