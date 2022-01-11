using Tinkoff.InvestAPI.V1;

namespace Tinkoff.InvestApi.Sample;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly InvestApiClient _investApi;

    public Worker(ILogger<Worker> logger, InvestApiClient investApi)
    {
        _logger = logger;
        _investApi = investApi;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var accounts = await _investApi.Users.GetAccountsAsync(new GetAccountsRequest());
        var userInfo = await _investApi.Users.GetInfoAsync(new GetInfoRequest(){});
        _logger.LogInformation(userInfo.ToString());
    }
}