using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;


namespace _2025_xunit_to_the_limits_src.T9_SocialAsyncContainers_HTTP;

public class MyWebAppFactory : WebApplicationFactory<T9webAPI.Program>
{
    
    //THIS IS A MAJOR IMPROVEMENT
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(svc =>
        {
   //         svc.AddTransient<IWeatherService, TestWeatherService>();
        });
    }
}