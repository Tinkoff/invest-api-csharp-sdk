using System;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Tinkoff.InvestApi.V1;

namespace Tinkoff.InvestApi.Tests;

[Collection(nameof(InvestApiClientFixtureCollection))]
public class InvestApiClientTests
{
    private readonly InvestApiClientFixture _fixture;

    public InvestApiClientTests(InvestApiClientFixture fixture)
    {
        _fixture = fixture;
    }

    private MockHttpMessageHandler Handler => _fixture.MockHttpMessageHandler;

    [Fact]
    public void Client_Initialization_NotThrows()
    {
        _fixture.Invoking(x => x.Client).Should().NotThrow();
    }

    [Fact]
    public async Task Client_Call_ToValidAddress()
    {
        Handler.Expect("https://invest-public-api.tinkoff.ru:443/*");
        await SendRequest();
        _fixture.MockHttpMessageHandler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Client_Call_WithAuthorization()
    {
        Handler.Expect("*").WithHeaders("Authorization", "Bearer token");
        await SendRequest();
        _fixture.MockHttpMessageHandler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Client_Call_WithAppName()
    {
        Handler.Expect("*").WithHeaders("x-app-name", "tinkoff.invest-api-csharp-sdk");
        await SendRequest();
        _fixture.MockHttpMessageHandler.VerifyNoOutstandingExpectation();
    }

    private async Task SendRequest()
    {
        try
        {
            await _fixture.Client.Users.GetInfoAsync(new GetInfoRequest());
        }
        catch (Exception)
        {
            // ignored
        }
    }
}