using _2025_xunit_to_the_limits_src.T4_Fixtures;
using FluentAssertions;

namespace _2025_xunit_to_the_limits_src.Intros;

public class T3_PrepareWithFixture : IClassFixture<SimpleSyncLifeTimeFixture>
{
    private readonly SimpleSyncLifeTimeFixture _fixture;

    public T3_PrepareWithFixture(SimpleSyncLifeTimeFixture fixture)
    {
        Console.WriteLine("entering ctor of " + nameof(T3_PrepareWithFixture));
         Console.WriteLine(fixture.TestableValue);
         _fixture = fixture;
    }

    [Fact]
    public void Test1()
    {
        Assert.True(true);
        _fixture.TestableValue.Should().Be(42);
    }

    // need to have that fixture
}

// parallelize it