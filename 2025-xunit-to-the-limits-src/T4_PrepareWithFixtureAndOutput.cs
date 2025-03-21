using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;


namespace _2025_xunit_to_the_limits_src;

public class T4_PrepareWithFixtureAndOutput  : IClassFixture<SimpleSyncLifeTimeFixture>
{
    private readonly SimpleSyncLifeTimeFixture _fixture;


    [Fact]
    public void Test1()
    {
        _fixture.TestableValue.Should().Be(42);
    }
    
    [Fact]
    public void TestClassExpectingLoggerButWhere()
    {
        var sutClass = new T4_SutClass(null);
        sutClass.TestableValue.Should().Be(88);
    }
    
    // we need to have that fixture
    public T4_PrepareWithFixtureAndOutput(SimpleSyncLifeTimeFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        outputHelper.WriteLine("fixture.TestableValue is " + fixture.TestableValue.ToString());
        
       // outputHelper is not ILogger 🤔
        
        
       // this.TestLogger = outputHelper.ToLogger<SimpleSyncLifeTime>();
    }

    public ILogger  TestLogger { get; init; }
    
    
    [Fact]
    public void TestClassExpectingLogger()
    {
        var sutClass = new T4_SutClass(this.TestLogger);
        sutClass.TestableValue.Should().Be(88);
    }
}