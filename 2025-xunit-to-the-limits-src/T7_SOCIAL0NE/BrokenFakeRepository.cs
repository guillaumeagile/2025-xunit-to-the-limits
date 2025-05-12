using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

public class BrokenFakeRepository<T> : IRepository<T>
{
    public bool Save(T anElement)
    {
        return false;
    }
}