using System.Text.Json;
using FluentAssertions;
using Microsoft.Playwright;


namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

[Collection(nameof(PlaywrightFixture))]
public class WeatherPageTests : IClassFixture<SharedPlaywrightCollection>, IAsyncLifetime
// although I've followed https://playwright.dev/dotnet/docs/api-testing
// there is support for inheriting PlaywrightTest in Xunit 😭, you must write a ClassFixture
{
    private readonly IPlaywright _playwright;
    private readonly IBrowser _browser;
    private readonly MyWebAppFactory waf;

    public WeatherPageTests(PlaywrightFixture fixture)
    {
        _playwright = fixture.PlaywrightInstance;
        _browser = fixture.Browser;
         waf = new MyWebAppFactory();
        waf.UseKestrel(cfg => { cfg.ListenLocalhost(1234); });
    }
    
    // DISCLAIMER : the code given in release note preview 4 is just CRAP
    // https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview4/aspnetcore.md#use-webapplicationfactory-with-kestrel-for-integration-testing

    [Fact]
    public async Task Page_LoadsSuccessfully()
    {
        var weatherPath = waf.ClientOptions.BaseAddress.ToString() + "weather";

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

    public Task InitializeAsync()
    {
        waf.StartServer(); // what???? no async !?
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await waf.DisposeAsync();
    }
}