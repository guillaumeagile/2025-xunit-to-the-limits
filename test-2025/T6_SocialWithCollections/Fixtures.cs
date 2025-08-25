using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections;

public class TestFixtureWithAnyRepo : IAmAFixture // must be a concrete class, but is designed as a base
{
    public IRepository<Element> Repository { get; protected set; }
    public void Dispose()
    { }
}

public class TestFixtureWithFake : TestFixtureWithAnyRepo
{
    public TestFixtureWithFake() => Repository = new BrokenFakeRepository<Element>();
}

public class TestFixtureWithDriver : TestFixtureWithAnyRepo
{
    public TestFixtureWithDriver() => Repository = new DriverRepository<Element>();
}

public interface IAmAFixture : IDisposable
{
}