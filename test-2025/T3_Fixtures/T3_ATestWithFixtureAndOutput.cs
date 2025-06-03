using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T3_Fixtures;
/*
 *
   ███████ ██ ██   ██ ████████ ██    ██ ██████  ███████ 
   ██      ██  ██ ██     ██    ██    ██ ██   ██ ██      
   █████   ██   ███      ██    ██    ██ ██████  █████   
   ██      ██  ██ ██     ██    ██    ██ ██   ██ ██      
   ██      ██ ██   ██    ██     ██████  ██   ██ ███████ 
                                                        
                                                        
     
                                                                                                                              
 https://patorjk.com/software/taag/#p=testall&h=3&v=1&f=Rectangles&t=Type%20Something%20                                                                                                                             
   
 */
public class T3_ATestWithFixtureAndOutput : IClassFixture<SimpleSyncLifeTimeFixture>
{
    private readonly SimpleSyncLifeTimeFixture _fixture;
    

    [Fact]
    [Trait("MyCategories", "T4_Fixtures")]
    public void Test1()
    {
        _fixture.TestableValue.Should().Be(42);
    }
    
    [Fact]
    [Trait("MyCategories", "T4_Fixtures")]
    public void Test3()
    {
        _fixture.TestableValue.Should().Be(42);
    }


    [Fact]
    public void TestClassExpectingLoggerButWhere()
    {
        var sutClass = new T4_SutClass(null);
        sutClass.TestableValue.Should().Be(88);
    }
    
    [Fact]
    public void TestClassExpectingLogger()
    {
        var sutClass = new T4_SutClass(this.TestLogger);
        sutClass.TestableValue.Should().Be(88);
    }

    [Fact]
    public void zzzz()
    {
        _fixture.TestableValue ++;  // NOOOOOOOOOO, fixture is steady across methods, transient across instances of classes
        _fixture.TestableValue.Should().Be(43);
    }
    
    // we need to have that fixture
    public T3_ATestWithFixtureAndOutput(SimpleSyncLifeTimeFixture fixture, ITestOutputHelper outputHelper)
    {
        outputHelper.WriteLine("welcome in T4_ATestWithFixtureAndOutput");
        _fixture = fixture;
        outputHelper.WriteLine("fixture.TestableValue is " + fixture.TestableValue.ToString());

        
        
        
        
        
        
        
        // outputHelper is not ILogger 🤔
      //   this.TestLogger = outputHelper.ToLogger<SimpleSyncLifeTimeFixture>();
    }

    public ILogger TestLogger { get; init; }





}