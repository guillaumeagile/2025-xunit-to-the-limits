using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using T8_Repositories_Adapters.source;


namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

public class MyWebAppFactory : WebApplicationFactory<T9webAPI.Program>
{
    private FakeStorageAdapter<SomeDto> _fakeStorageAdapter;

    public FakeStorageAdapter<SomeDto> FakeStorageAdapter => _fakeStorageAdapter;

    //THIS IS A MAJOR IMPROVEMENT
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(svc =>
        {
            _fakeStorageAdapter = new FakeStorageAdapter<SomeDto>();
            svc.AddTransient<IStorageAdapter<SomeDto>>(sp => _fakeStorageAdapter);
        });
    }
}