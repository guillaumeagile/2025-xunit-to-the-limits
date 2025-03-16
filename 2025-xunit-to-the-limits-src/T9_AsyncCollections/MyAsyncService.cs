using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE;
using Microsoft.Extensions.Logging;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections;

public class MyAsyncService
{
    private readonly IAsyncRepository<Element> _repository;
    private readonly ILogger _logger;

    public MyAsyncService(IAsyncRepository<Element> repository, ILogger logger)
    {
        logger.LogInformation("logger available in MyAsyncService");
         _repository = repository;
         _logger = logger;
    }


    
    public async Task<bool> SaveSocialAsync(Element anElement)
    {
        _logger.LogInformation("begin task in SaveSocial");
        return await _repository.SaveAsync(anElement);
        _logger.LogInformation("end task in SaveSocial");
        
    }
}