using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T8_SocialWithCollections;

[CollectionDefinition(nameof(CollectionOfTestsWithFake))]
public class CollectionOfTestsWithFake : ICollectionFixture<TestFixtureWithFake>
{
}

public class TestFixtureWithAnyRepo
{
    public IRepository<Element> Repository { get; protected set; }
}

public class TestFixtureWithFake : TestFixtureWithAnyRepo
{
    public TestFixtureWithFake() => Repository = new BrokenFakeRepository<Element>();
}

public class TestFixtureWithDriver : TestFixtureWithAnyRepo
{
    public TestFixtureWithDriver() => Repository = new DriverRepository<Element>();
}

[CollectionDefinition(nameof(CollectionOfTestsWithDriver))]
public class CollectionOfTestsWithDriver : ICollectionFixture<TestFixtureWithDriver>
{
}