using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Options;
using Tinkoff.InvestApi;

namespace Microsoft.Extensions.DependencyInjection;

public static class InvestApiClientExtensions
{
    public static IServiceCollection AddInvestApiClient(this IServiceCollection services, string accessToken)
    {
        services.AddOptions<InvestApiSettings>().Configure(x => x.AccessToken = accessToken);
        return services.AddInvestApiClient();
    }
    
    public static IServiceCollection AddInvestApiClient(this IServiceCollection services)
    {
        services.AddSingleton<IValidateOptions<InvestApiSettings>>(new ValidateOptions<InvestApiSettings>(string.Empty,
            settings => !string.IsNullOrWhiteSpace(settings.AccessToken), "AccessToken is required"));

        services.AddGrpcClient<InvestApiClient>(o => o.Address = new Uri("https://invest-public-api.tinkoff.ru:443"))
            .ConfigureChannel((serviceProvider, options) =>
            {
                var accessToken = serviceProvider.GetRequiredService<IOptions<InvestApiSettings>>().Value.AccessToken;
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