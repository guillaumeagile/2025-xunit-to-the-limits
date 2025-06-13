using Microsoft.Playwright;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

[CollectionDefinition(nameof(PlaywrightFixture))]
public class SharedPlaywrightCollection : ICollectionFixture<PlaywrightFixture> {}

// ReSharper disable once ClassNeverInstantiated.Global
public class PlaywrightFixture : IAsyncLifetime
{
    public virtual async Task InitializeAsync()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync();
    }
    // its aim is to "carry" the Playwright tooling
    public IBrowser Browser { get; private set; } = null!;
    public IPlaywright PlaywrightInstance { get; private set; } = null!;

    public virtual async Task DisposeAsync()
    {
        await Browser.DisposeAsync();
        PlaywrightInstance.Dispose();
    }
}