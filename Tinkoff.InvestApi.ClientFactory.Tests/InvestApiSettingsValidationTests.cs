using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tinkoff.InvestApi.ClientFactory.Tests;

public class InvestApiSettingsValidationTests
{
    private readonly ServiceCollection _services = new();

    public InvestApiSettingsValidationTests()
    {
        _services.AddInvestApiClient();
    }

    private ServiceProvider ServiceProvider => _services.BuildServiceProvider();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void EmptyAccessToken_Throws(string accessToken)
    {
        _services.AddOptions<InvestApiSettings>().Configure(x => x.AccessToken = accessToken);
        FluentActions.Invoking(() => ServiceProvider.GetService<IOptions<InvestApiSettings>>().Value)
            .Should().Throw<OptionsValidationException>();
    }

    [Fact]
    public void NotEmptyAccessToken_NotThrows()
    {
        _services.AddOptions<InvestApiSettings>().Configure(x => x.AccessToken = "token");
        FluentActions.Invoking(() => ServiceProvider.GetService<IOptions<InvestApiSettings>>().Value)
            .Should().NotThrow<OptionsValidationException>();
    }

    [Fact]
    public void WithoutInvestApiSettings_Throws()
    {
        FluentActions.Invoking(() => ServiceProvider.GetService<IOptions<InvestApiSettings>>().Value)
            .Should().Throw<OptionsValidationException>();
    }
}