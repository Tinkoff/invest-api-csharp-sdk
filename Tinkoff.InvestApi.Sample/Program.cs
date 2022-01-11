using Microsoft.Extensions.Configuration.UserSecrets;
using Tinkoff.InvestApi.Sample;

[assembly: UserSecretsId("dotnet-Tinkoff.InvestApi.Sample-D6A3E63B-22F2-4DD0-BD53-B1C61D66D657")]

var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>()
            .Configure<InvestApiSettings>(context.Configuration)
            .AddInvestApiClient();
    })
    .Build();
await host.RunAsync();