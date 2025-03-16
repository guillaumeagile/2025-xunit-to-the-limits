using NUlid;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

[CollectionDefinition(nameof(CollectionOfTestsWitFake))]
public class CollectionOfTestsWitFake  : ICollectionFixture<TestFixtureWithFake>
{
}

public class TestFixtureWithFake
{
    public TestFixtureWithFake()
    {
         Repository = new BrokenFakeRepository<Element>();
    }

    public IRepository<Element> Repository { get; init; }
}
