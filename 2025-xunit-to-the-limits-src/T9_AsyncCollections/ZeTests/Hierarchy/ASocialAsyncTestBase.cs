using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T9_AsyncCollections.TheSUT;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections.ZeTests.Hierarchy;

public abstract class SocialAsyncTestBase : IClassFixture<TestFixtureWithAnyAsyncRepo>, IAsyncLifetime
{
    /*********** the TEST SUITE *********/
    
    [Fact]
    public async Task SocialPalGivenByFixture()
    {
        var anElement = new Element();

        var resOfSave = await _sut.SaveSocialAsync(anElement);

        resOfSave.Should().BeTrue();
        
        _logger.LogTrace( _fixture.Uid);
    }
    
    private readonly ILogger _logger;
    private readonly MyAsyncService _sut;
    private readonly TestFixtureWithAnyAsyncRepo _fixture;

    protected SocialAsyncTestBase(TestFixtureWithAnyAsyncRepo fixture, ITestOutputHelper outputHelper)
    {
        _logger = outputHelper.ToLogger<SocialAsyncTestBase>();
        fixture.TestLogger = _logger;
        _fixture = fixture;
        _sut = new MyAsyncService(fixture.Repository, _logger);
    }


    public async Task InitializeAsync()
    {
        _logger.LogTrace("SocialAsyncTestBase InitializeAsync");
    }

    public async Task DisposeAsync()
    {
        _logger.LogTrace("SocialAsyncTestBase DisposeAsync");
    }


  
}