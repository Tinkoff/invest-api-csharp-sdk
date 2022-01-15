using System;
using Microsoft.Extensions.DependencyInjection;

namespace Tinkoff.InvestApi.ClientFactory.Tests;

public class InvestApiSettingsValidationTests
{
    [Fact]
    public void AddInvestApiClient_NullAccessToken_Throws()
    {
        var serviceProvider = new ServiceCollection()
            .AddInvestApiClient((_, _) => { })
            .BuildServiceProvider();
        
        serviceProvider.Invoking(x => x.GetService<InvestApiClient>())
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("AccessToken is required");
    }
}