using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using T8_Repositories_Adapters.source;

namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_withDSL.Tooling;

internal class WafWithMongoAdapter  : WebApplicationFactory<T9webAPI.Program>
{
    private IStorageAdapter<SomeDto> _mongoStorageAdapter;
    private MongoDbConnection _mongoDbConnection;
    
    public IStorageAdapter<SomeDto> MongoStorageAdapter => _mongoStorageAdapter;

    public WafWithMongoAdapter(MongoDbConnection mongoDbConnection)
    {
        _mongoDbConnection = mongoDbConnection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(svc =>
        {
            _mongoStorageAdapter = new MongoStorageAdapter<SomeDto>(_mongoDbConnection);
     //       _fakeStorageAdapter = new FakeStorageAdapter<SomeDto>();  // to keep track of the adapter being injected
            svc.AddSingleton<IStorageAdapter<SomeDto>>(sp => _mongoStorageAdapter);
        });
    }
}