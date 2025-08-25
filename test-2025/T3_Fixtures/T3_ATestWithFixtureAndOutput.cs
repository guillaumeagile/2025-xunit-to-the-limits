using AwesomeAssertions;
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
public class T3_ATestWithFixtureAndOutput : IClassFixture<ZeSimpleSyncLifeTimeFixture>
{
    private readonly ZeSimpleSyncLifeTimeFixture _fixture;
    private string _storedUid;

    [Fact]
    public void Test1()
    {
        _fixture.TestableValue.Should().Be(42);
        _storedUid = _fixture.Uid; // you shouldn't write stuff like that
    }
    
    [Fact]
    public void Test2()
    {
        _fixture.TestableValue.Should().Be(42);
        //_fixture.Uid.Should().Be(_storedUid);
    }
   
    [Fact]
    public void __DontDoThat()
    {
        _fixture.TestableValue ++;  // NOOOOOOOOOO, fixture is steady across methods, transient across instances of classes
        _fixture.TestableValue.Should().Be(43);
    }
    
    [Fact]
    public void TestClassExpectingLogger()
    {
        var sutClass = new T3_SutClass(this.TestLogger);
        sutClass.TestableValue.Should().Be(88);
    }

    
    // we need to have that fixture
    public T3_ATestWithFixtureAndOutput(ZeSimpleSyncLifeTimeFixture fixture, ITestOutputHelper outputHelper)
    {
        outputHelper.WriteLine("welcome in T4_ATestWithFixtureAndOutput");
        _fixture = fixture;
        outputHelper.WriteLine("fixture.TestableValue is " + fixture.TestableValue.ToString());
        
        Console.WriteLine("you will never see this 👻 👻 👻");

        
        
        
        
        
        
        
        // outputHelper is not ILogger 🤔
        // this.TestLogger = outputHelper.ToLogger<ZeSimpleSyncLifeTimeFixture>();
    }

    public ILogger TestLogger { get; init; }





}