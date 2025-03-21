using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public class TestFixtureWithAnyAsyncRepo : IAsyncLifetime
{
    public IAsyncRepository<Element> Repository { get; set; } = null!;
    public ILogger TestLogger { get; set; } = NullLogger.Instance;


    public virtual  Task InitializeAsync()
    {
        // initialize async arrives first, before the construction of the test class
        TestLogger.LogInformation("YOU WILL NEVER SEE THIS");
      return Task.CompletedTask; 
    }

    public virtual Task DisposeAsync()
    {
        return Task.CompletedTask; 
        
    }
}