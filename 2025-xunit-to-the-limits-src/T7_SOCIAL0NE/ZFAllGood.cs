using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T8_SocialWithCollections;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

[CollectionDefinition(nameof(CollectionOfTestsWithFakeOk))]
public class CollectionOfTestsWithFakeOk : ICollectionFixture<TestFixtureWithFakeOk>
{
}

public class TestFixtureWithFakeOk : ITestFixtureWithRepository
{
    public TestFixtureWithFakeOk()
    {
        Repository = new DriverRepository <Element>();
    }

    public IRepository<Element> Repository { get; init; }
}