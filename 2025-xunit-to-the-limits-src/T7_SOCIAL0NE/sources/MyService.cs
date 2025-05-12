namespace _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;

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