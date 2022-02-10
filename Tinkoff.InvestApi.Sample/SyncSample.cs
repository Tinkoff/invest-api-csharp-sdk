namespace Tinkoff.InvestApi.Sample;

public class SyncSample : BackgroundService
{
    private readonly InvestApiClient _investApi;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ILogger<AsyncSample> _logger;

    public SyncSample(ILogger<AsyncSample> logger, InvestApiClient investApi, IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _investApi = investApi;
        _lifetime = lifetime;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var userInfoDescription = new UsersServiceSample(_investApi).GetUserInfoDescription();
        _logger.LogInformation(userInfoDescription);

        var instrumentsDescription = new InstrumentsServiceSample(_investApi.Instruments)
            .GetInstrumentsDescription();
        _logger.LogInformation(instrumentsDescription);

        var operationsDescription = new OperationsServiceSample(_investApi)
            .GetOperationsDescription();
        _logger.LogInformation(operationsDescription);

        _lifetime.StopApplication();

        return Task.CompletedTask;
    }
}