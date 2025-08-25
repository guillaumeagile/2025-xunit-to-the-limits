using System.Text.Json;
using Microsoft.Playwright;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.TestCollections;

public class DslFixtureWResponse(IAPIResponse response) : IAsyncDisposable, IDisposable
{
    public IAPIResponse Response => response;
    
    public async  Task<JsonElement?> ExtractJsonAsync()
    {
        return await response.JsonAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await response.DisposeAsync();
    }

    public void Dispose()
    {
        DisposeAsync().AsTask().Wait();
    }
}