namespace Tinkoff.InvestApi.Sample;

public class AsyncSample : BackgroundService
{
    private readonly InvestApiClient _investApi;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ILogger<AsyncSample> _logger;

    public AsyncSample(ILogger<AsyncSample> logger, InvestApiClient investApi, IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _investApi = investApi;
        _lifetime = lifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var userInfoDescription = await new UsersServiceSample(_investApi).GetUserInfoDescriptionAsync(stoppingToken);
        _logger.LogInformation(userInfoDescription);

        var instrumentsDescription = await new InstrumentsServiceSample(_investApi.Instruments)
            .GetInstrumentsDescriptionAsync(stoppingToken);
        _logger.LogInformation(instrumentsDescription);

        var operationsDescription = await new OperationsServiceSample(_investApi)
            .GetOperationsDescriptionAsync(stoppingToken);
        _logger.LogInformation(operationsDescription);

        _lifetime.StopApplication();
    }
}