using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Tinkoff.InvestApi;

namespace Microsoft.Extensions.DependencyInjection;

public static class InvestApiClientExtensions
{
    private const string DefaultName = "";

    public static IServiceCollection AddInvestApiClient(this IServiceCollection services,
        Action<IServiceProvider, InvestApiSettings> configureSettings)
    {
        return AddInvestApiClient(services, DefaultName, configureSettings);
    }

    public static IServiceCollection AddInvestApiClient(this IServiceCollection services, string name,
        Action<IServiceProvider, InvestApiSettings> configureSettings)
    {
        services.AddGrpcClient<InvestApiClient>(name,
                o => o.Address = new Uri("https://invest-public-api.tinkoff.ru:443"))
            .ConfigureChannel((serviceProvider, options) =>
            {
                var settings = new InvestApiSettings();
                configureSettings(serviceProvider, settings);
                var accessToken = settings.AccessToken ??
                                  throw new InvalidOperationException("AccessToken is required");
                var credentials = CallCredentials.FromInterceptor((_, metadata) =>
                {
                    metadata.Add("Authorization", $"Bearer {accessToken}");
                    metadata.Add("x-app-name", "tinkoff.invest-api-csharp-sdk");
                    return Task.CompletedTask;
                });

                options.Credentials = ChannelCredentials.Create(new SslCredentials(), credentials);

                var defaultMethodConfig = new MethodConfig
                {
                    Names = {MethodName.Default},
                    RetryPolicy = new RetryPolicy
                    {
                        MaxAttempts = 5,
                        InitialBackoff = TimeSpan.FromSeconds(1),
                        MaxBackoff = TimeSpan.FromSeconds(5),
                        BackoffMultiplier = 1.5,
                        RetryableStatusCodes = {StatusCode.Unavailable}
                    }
                };

                options.ServiceConfig = new ServiceConfig {MethodConfigs = {defaultMethodConfig}};
            });
        return services;
    }
}