using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Playwright;
using T8_Repositories_Adapters.source;


namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

[Collection(nameof(PlaywrightFixture))]
public class ApiSimpleTests : IClassFixture<SharedPlaywrightCollection>, IAsyncLifetime
// although I've followed https://playwright.dev/dotnet/docs/api-testing
// there is support for inheriting PlaywrightTest in Xunit 😭, you must write a ClassFixture
{
    private readonly IPlaywright _playwright;
    private readonly IBrowser _browser;
    private readonly MyWebAppFactory _waf;

    public ApiSimpleTests(PlaywrightFixture fixture)
    {
        _playwright = fixture.PlaywrightInstance;
        _browser = fixture.Browser;
         _waf = new MyWebAppFactory();
        _waf.UseKestrel(cfg => { cfg.ListenLocalhost(1234); });
        _waf.StartServer();
    }
    
    // DISCLAIMER : the code given in release note preview 4 is just CRAP
    // https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview4/aspnetcore.md#use-webapplicationfactory-with-kestrel-for-integration-testing

    [Fact]
    public async Task Page_LoadsSuccessfully()
    {
        var weatherPath = _waf.ClientOptions.BaseAddress.ToString() + "weather";

        var ctx = await _playwright.APIRequest.NewContextAsync();
        var response = await ctx.GetAsync(weatherPath);

        response.Ok.Should().BeTrue();
        var json = (await response.JsonAsync())
            .Value
            .Should().BeOfType<JsonElement>()
            .Subject;

        json[0].GetProperty("temperatureC").GetInt32().Should().BeInRange(-20, 55);
        json[0].GetProperty("temperatureF").GetInt32().Should().BeGreaterThan(0);
    }
    
    [Fact]
    public async Task CallRouteGetById()
    {
        //ARRANGE the data in the storage
      // var storageAdapter =  waf.Services.GetService(typeof(IStorageAdapter<SomeDto>)); boooooooh...  NULL :(
      var storageAdapter = _waf.FakeStorageAdapter; 
       
        storageAdapter.Should().NotBeNull();
        var someDto = new SomeDto("2", "Foo", 20);
        await ((FakeStorageAdapter<SomeDto>)storageAdapter).InsertOrUpdateAsync(someDto, CancellationToken.None);
        
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

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _waf.DisposeAsync();
    }
}