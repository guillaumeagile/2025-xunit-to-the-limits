using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.Fixtures;

public interface ITestFixtureWithRepository
{
    IRepository<Element> Repository { get; init; }
}