using System.Text.Json;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

// Получаем токен из переменной окружения
var token = Environment.GetEnvironmentVariable("TOKEN");

// Собираем ServiceCollection с клиентом
var serviceCollection = new ServiceCollection();
serviceCollection.AddInvestApiClient((_, settings) =>
{
    settings.AccessToken = token;
});
var serviceProvider = serviceCollection.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<InvestApiClient>();


var stream = client.MarketDataStream.MarketDataStream();
// Отправляем запрос в стрим
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
// Обрабатываем все приходящие из стрима ответы
await foreach (var response in stream.ResponseStream.ReadAllAsync())
{
    Console.WriteLine(JsonSerializer.Serialize(response));
}
