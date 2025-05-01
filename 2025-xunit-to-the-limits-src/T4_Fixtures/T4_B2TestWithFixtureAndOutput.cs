using _2025_xunit_to_the_limits_src.Intros;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace _2025_xunit_to_the_limits_src.T4_Fixtures;

public class T4_B2TestWithFixtureAndOutput : IClassFixture<SimpleSyncLifeTimeFixture>
{
    private readonly SimpleSyncLifeTimeFixture _fixture;


    [Fact]
    [Trait("MyCategories", "T4_Fixtures")]
    public void Test1()
    {
        _fixture.TestableValue.Should().Be(42);
    }


    [Fact]
    //[Trait("MyCategories", "T4_Fixtures")]
    //[Fact(Skip = "vilain petit canard")] 
    public void TestModifySomethingInTheFixture_Booooooh()
    {
        _fixture.TestableValue *= 2;
        _fixture.TestableValue.Should().Be(84);
    }

    // we need to have that fixture
    public T4_B2TestWithFixtureAndOutput(SimpleSyncLifeTimeFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
    }


    [Fact]
    [Trait("MyCategories", "T4_Fixtures")]
    public void Test1_Mod()
    {
        _fixture.TestableValue++;
        _fixture.TestableValue.Should().Be(43);
    }
}