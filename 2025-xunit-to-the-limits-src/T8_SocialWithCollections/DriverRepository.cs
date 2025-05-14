using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T8_SocialWithCollections;

public class DriverRepository<T> : IRepository<Element>
{
    public bool Save(Element anElement)
    {
        Thread.Sleep(500);
        return true;
    }
}