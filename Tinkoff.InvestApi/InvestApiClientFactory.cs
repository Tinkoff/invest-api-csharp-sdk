using Microsoft.Extensions.DependencyInjection;

namespace Tinkoff.InvestApi;

public static class InvestApiClientFactory
{
    public static InvestApiClient Create(string accessToken)
    {
        return new ServiceCollection()
            .AddInvestApiClient((_, settings) => settings.AccessToken = accessToken)
            .BuildServiceProvider()
            .GetRequiredService<InvestApiClient>();
    }
}