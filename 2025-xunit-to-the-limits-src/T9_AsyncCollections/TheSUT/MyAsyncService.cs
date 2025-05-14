using _2025_xunit_to_the_limits_src.T7_SOCIAL0NE.sources;
using Microsoft.Extensions.Logging;

namespace _2025_xunit_to_the_limits_src.T9_AsyncCollections.TheSUT;

public class MyAsyncService
{
    private readonly ILogger _logger;
    private readonly IAsyncRepository<Element> _repository;

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
    }
}