using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;

namespace Tinkoff.InvestApi.ClientFactory.Tests;

public class InvestApiClientFixture
{
    internal readonly MockHttpMessageHandler MockHttpMessageHandler = new();
    public ServiceProvider ServiceProvider { get; set; }

    public InvestApiClient Client => ServiceProvider.GetRequiredService<InvestApiClient>();

    public InvestApiClientFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInvestApiClient("token");
        serviceCollection.AddOptions<GrpcClientFactoryOptions>(nameof(InvestApiClient))
            .PostConfigure(factoryOptions => factoryOptions.ChannelOptionsActions.Add(options =>
            {
                options.HttpHandler = MockHttpMessageHandler;
            }));
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}