using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;
using _2025_xunit_to_the_limits_src.T6_SocialWithCollections;

namespace _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;


public class TestFixtureWithRepo : ITestFixtureWithRepository
{
    public TestFixtureWithRepo()
    {
        Repository = new DriverRepository <Element>();
    }

    public IRepository<Element> Repository { get; init; }
}