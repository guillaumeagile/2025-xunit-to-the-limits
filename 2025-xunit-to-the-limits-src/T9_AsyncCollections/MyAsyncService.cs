using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public class MyAsyncService
{
    private readonly IAsyncRepository<Element> _repository;

    public MyAsyncService()
    {
        
    }

    public MyAsyncService(IAsyncRepository<Element> repository)
    {
    _repository = repository;
    }


    
    public async Task<bool> SaveSocial(Element anElement)
    {
        return await _repository.SaveAsync(anElement);
    }
}