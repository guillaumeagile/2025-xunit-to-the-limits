using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T8_SocialWithCollections;

[CollectionDefinition(nameof(CollectionOfTestsWithFake))]
public class CollectionOfTestsWithFake : ICollectionFixture<TestFixtureWithFake>
{
}

[CollectionDefinition(nameof(AnotherCollectionOfTestsWithFake))] //same fixture
public class AnotherCollectionOfTestsWithFake : ICollectionFixture<TestFixtureWithFake>
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

// will be used when removing the comment in A0_SocialTestCollectable
[CollectionDefinition(nameof(CollectionOfTestsWithDriver))]
public class CollectionOfTestsWithDriver : ICollectionFixture<TestFixtureWithDriver>
{
}