using _2025_xunit_to_the_limits_src.Intros;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T4_Fixtures;

public class T3_PrepareWithFixtureAndOutput  : IClassFixture<SimpleSyncLifeTimeOutputFixture>
{
    private readonly SimpleSyncLifeTimeOutputFixture _fixture;


    [Fact]
    public void Test1()
    {
        //_fixture.TestableValue.Should().Be(42);
    }
    
   
    
    // we need to have that fixture
    public T3_PrepareWithFixtureAndOutput(SimpleSyncLifeTimeOutputFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        outputHelper.WriteLine("fixture.TestableValue is " );
        
       // outputHelper is not ILogger 🤔
        this.TestLogger = outputHelper.ToLogger<SimpleSyncLifeTimeFixture>();
    }

    public ILogger  TestLogger { get; init; }
    
    
    [Fact]
    public void TestClassExpectingLogger()
    {
        var sutClass = new T4_SutClass(this.TestLogger);
        sutClass.TestableValue.Should().Be(88);
    }
}

public class SimpleSyncLifeTimeOutputFixture
{
    public ITestOutputHelper TestOutput { get; private set ; }
/*
    public SimpleSyncLifeTimeOutput()
    {        
    }
    */


    //Given the existing design, it isn't possible, because ITestOutputHelper is scoped to the execution of a single test.
    // https://github.com/xunit/xunit/discussions/2391
    public SimpleSyncLifeTimeOutputFixture( ITestOutputHelper outputHelper)
    {
        TestOutput = outputHelper;
    }
}