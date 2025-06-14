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
        //  _waf.StartServer(); 
        await this._fixtureDsl.InitializeAsync(); // HERE !!!! super important to AWAIT for this
        // the fixture is reInitialized everytime, which create new containers... SLOWER :(

        //BENEFIT: the plumbing is in the DSL, and easier to share across tests
        // this also the reduce the surface of friction between test plumbing (which was too big)
        // and limit the misusage of all the stuff: playwright+waf+testContainers
        // the DSL rules them all 💪
    }


    
    [Fact]
    public async Task CallRouteGetById_Concise()
    {
        var someDto = new SomeDto("2", "Foobar", 42);
        
        await _fixtureDsl
            .InsertSome(someDto)
            .ContinueWith(async _ =>
            {
                await using var result = await _fixtureDsl
                    .SetRelativePathTo("stored/2")
                    .GetAllAsync();
                
                var jsonElement = await result.ExtractJsonAsync();
                jsonElement.HasValue.Should().BeTrue();
                
                var actualdDto = JsonSerializer.Deserialize<SomeDto>(jsonElement.ToString());
                actualdDto.Should().BeEquivalentTo(someDto);
            });
    }
    


    public async Task DisposeAsync()
    {
        await _fixtureDsl.DisposeAsync();
    }
}