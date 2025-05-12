using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using NUlid;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

[CollectionDefinition(nameof(CollectionOfTestsWithFake))]
public class CollectionOfTestsWithFake : ICollectionFixture<TestFixtureWithFake>
{
}

public class TestFixtureWithFake : ITestFixtureWithRepository
{
    public TestFixtureWithFake()
    {
        Repository = new BrokenFakeRepository<Element>();
    }

    public IRepository<Element> Repository { get; init; }
}

public interface ITestFixtureWithRepository
{
    IRepository<Element> Repository { get; init; }
}