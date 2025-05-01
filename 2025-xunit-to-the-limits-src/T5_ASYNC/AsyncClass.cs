using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace _2025_xunit_to_the_limits_src.T5_ASYNC;

public class AsyncClass
{
    private readonly ILogger _logger;
    public AsyncClass(ILogger logger) => _logger = logger;

    public void SyncCompute(string filePath)
    {
        _logger.LogInformation("***** begin SyncCompute ");
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
        _ = MD5.HashData(stream);
        _logger.LogInformation("finished SyncCompute *****");
    }

    public async Task<string> ASyncCompute(string filePath)
    {
        _logger.LogInformation("***** begin ASyncCompute ");

        await using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
        var res = await MD5.HashDataAsync(stream, CancellationToken.None);
        ;
        _logger.LogInformation("finished ASyncCompute *****");
        return Convert.ToBase64String(res);
    }
}