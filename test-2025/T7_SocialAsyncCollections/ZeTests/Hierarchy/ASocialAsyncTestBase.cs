using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.TheSUT;
using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T7_SocialAsyncCollections.ZeTests.Hierarchy;

/*********** the TEST SUITE *********/
[Trait("Category", "BaseClass")]
public abstract class SocialAsyncTestBase : IAsyncLifetime  //: IClassFixture<TestFixtureWithAnyRepo>
                                                            // 💡 yes, removing that will prevent the test base from running 👍
{
    // only one here, but of course, you can have more ...
    [Fact]
    public async Task SavingWithTheHelpOfFriend()
    {
        var anElement = new Element();

        var resOfSave = await _sut.SaveSocialAsync(anElement);

        resOfSave.Should().BeTrue();
        
        _logger.LogTrace( _zeFixture.Uid);
    }
    
    private readonly ILogger _logger;
    private readonly MyAsyncService _sut;
    private readonly TestFixtureWithAnyAsyncRepo _zeFixture;

    protected SocialAsyncTestBase(TestFixtureWithAnyAsyncRepo zeFixture, ITestOutputHelper outputHelper)
    {
        _logger = outputHelper.ToLogger<SocialAsyncTestBase>();
        zeFixture.TestLogger = _logger;
        _zeFixture = zeFixture;
        _sut = new MyAsyncService(zeFixture.Repository, _logger);
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