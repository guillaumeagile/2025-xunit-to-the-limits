
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;


namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;


//the runner will not run an abstract class
public abstract class SocialAsyncTestBase : IClassFixture<TestFixtureWithAnyAsyncRepo>, IAsyncLifetime
{
    private readonly MyAsyncService sut;
    private readonly ILogger _logger;

    public SocialAsyncTestBase(TestFixtureWithAnyAsyncRepo fixture)
    {
        _logger = fixture.TestLogger;
        sut = new MyAsyncService  (fixture.Repository);
    }
    
    
    public async Task InitializeAsync()
    {

    }


    [Fact]
    public async Task SocialPalGivenByFixture()
    {
        var anElement = new Element();
   
        var resOfSave =  await  sut.SaveSocial(anElement);
        
        resOfSave.Should().BeFalse();
    }

    public async Task DisposeAsync()
    {

    }
}

[Collection(nameof(TestFixtureWithAsyncFake))]
public class SocialAsyncTestWithFake : SocialAsyncTestBase, IClassFixture<TestFixtureWithAsyncFake>
{
    public SocialAsyncTestWithFake(TestFixtureWithAsyncFake fixture ) : base(fixture)
    {
    }
}

public class TestFixtureWithAsyncFake : TestFixtureWithAnyAsyncRepo
{
    public TestFixtureWithAsyncFake(  ) : base(  )
    {
        //this one is fake and not really relies on async startup
        Repository = new AsyncFakeRepository<Element>(); 
    }

 
}

[Collection(nameof(TestFixtureWithAsyncDriver))]
public class SocialAsyncTestWithDriver : SocialAsyncTestBase, IClassFixture<TestFixtureWithAsyncDriver>
{
    public SocialAsyncTestWithDriver(TestFixtureWithAsyncDriver fixture ) : base(fixture)
    {
    }
}

public class TestFixtureWithAsyncDriver : TestFixtureWithAnyAsyncRepo
{
    public TestFixtureWithAsyncDriver(  ) : base(  )
    {
    }
    
    public override  async   Task InitializeAsync()
    {
          Repository = new AsyncDriverRepository<Element>();
          
          //the driver as an awaitable method to boot up
          await Task.Delay(500);
    }
}