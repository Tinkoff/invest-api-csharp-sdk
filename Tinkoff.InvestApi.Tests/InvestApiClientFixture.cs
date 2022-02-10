using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;

namespace Tinkoff.InvestApi.Tests;

public class InvestApiClientFixture
{
    internal readonly MockHttpMessageHandler MockHttpMessageHandler = new();

    public InvestApiClientFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInvestApiClient((_, settings) => settings.AccessToken = "token");
        serviceCollection.AddOptions<GrpcClientFactoryOptions>("")
            .PostConfigure(factoryOptions => factoryOptions.ChannelOptionsActions.Add(options =>
            {
                options.HttpHandler = MockHttpMessageHandler;
            }));
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; set; }

    public InvestApiClient Client => ServiceProvider.GetRequiredService<InvestApiClient>();
}