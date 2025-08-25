using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using T8_Repositories_Adapters.source;
using T8_Repositories_Adapters.sourceT10;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

public class WafWithRedisAdapter : WebApplicationFactory<T9webAPI.Program>
{
    private IStorageAdapter<SomeDto> _redisStorageAdapter;
    private RedisConnection _redisConnection;
    
    public IStorageAdapter<SomeDto> RedisStorageAdapter => _redisStorageAdapter;

    public WafWithRedisAdapter(string redisConnectionString)
    {
        _redisConnection = new RedisConnection(redisConnectionString);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(svc =>
        {
            // INJECTION WITH THE CONNECTION OBTAINED FROM THE FIXTURE => testcontainers in Action, once again 
            _redisStorageAdapter = new RedisStorageAdapter<SomeDto>(_redisConnection);
            
            svc.AddSingleton<IStorageAdapter<SomeDto>>(sp => _redisStorageAdapter);
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _redisConnection?.Dispose();
        }
        base.Dispose(disposing);
    }
}
