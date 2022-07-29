using System.Text.Json;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

var token = Environment.GetEnvironmentVariable("TOKEN");
var serviceCollection = new ServiceCollection();

serviceCollection.AddInvestApiClient((_, settings) =>
{
    settings.AccessToken = token;
});

var serviceProvider = serviceCollection.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<InvestApiClient>();

var stream = client.MarketDataStream.MarketDataStream();
await stream.RequestStream.WriteAsync(new MarketDataRequest
{
    SubscribeCandlesRequest = new SubscribeCandlesRequest
    {
        Instruments =
        {
            new CandleInstrument
            {
                Figi = "BBG004730N88",
                Interval = SubscriptionInterval.OneMinute
            }
        }
    }
});
await foreach (var response in stream.ResponseStream.ReadAllAsync())
{
    Console.WriteLine(JsonSerializer.Serialize(response));
}
