using Microsoft.Extensions.DependencyInjection;

namespace Tinkoff.InvestApi;

public static class InvestApiClientFactory
{
    public static InvestApiClient Create(string accessToken, bool sandbox = false)
    {
        return new ServiceCollection()
            .AddInvestApiClient((_, settings) =>
            {
                settings.AccessToken = accessToken;
                settings.Sandbox = sandbox;
            })
            .BuildServiceProvider()
            .GetRequiredService<InvestApiClient>();
    }  
    
    public static InvestApiClient Create(InvestApiSettings settings)
    {
        return new ServiceCollection()
            .AddInvestApiClient((_, clientSettings) =>
            {
                clientSettings.AccessToken = settings.AccessToken;
                clientSettings.Sandbox = settings.Sandbox;
                clientSettings.AppName = settings.AppName;
            })
            .BuildServiceProvider()
            .GetRequiredService<InvestApiClient>();
    }
}