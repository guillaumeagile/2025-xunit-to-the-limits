using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;



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