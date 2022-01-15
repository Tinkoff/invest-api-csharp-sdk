using Microsoft.Extensions.Configuration.UserSecrets;
using Tinkoff.InvestApi.Sample;

[assembly: UserSecretsId("2323bae0-f4bf-4c7b-90ce-1b87d3fd76a8")]

var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>()
            .AddInvestApiClient((_, settings) => context.Configuration.Bind(settings));
    })
    .Build();
await host.RunAsync();