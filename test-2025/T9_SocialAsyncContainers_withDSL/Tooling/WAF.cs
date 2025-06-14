using _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using T8_Repositories_Adapters.source;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.Tooling;

public class MyWebAppFactory : WebApplicationFactory<T9webAPI.Program>
{
    private FakeStorageAdapter<SomeDto> _fakeStorageAdapter;

    // to keep track of the adapter being injected
    public FakeStorageAdapter<SomeDto> FakeStorageAdapter => _fakeStorageAdapter;


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(svc =>
        {
            _fakeStorageAdapter = new FakeStorageAdapter<SomeDto>();  // to keep track of the adapter being injected
            svc.AddSingleton<IStorageAdapter<SomeDto>>(sp => _fakeStorageAdapter);
        });
    }
}