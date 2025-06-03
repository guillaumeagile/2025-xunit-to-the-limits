using _2025_xunit_to_the_limits_src.T5_SOCIAL0NE.sources;

namespace _2025_xunit_to_the_limits_src.T6_SocialWithCollections;

public class MyService
{
    private readonly IRepository<Element> _repository;

    public MyService()
    {
    }

    public MyService(IRepository<Element> repository)
    {
        _repository = repository;
    }


    public bool SaveAlone(Element anElement)
    {
        return true;
    }

    public bool SaveSocial(Element anElement)
    {
        return _repository.Save(anElement);
    }
}