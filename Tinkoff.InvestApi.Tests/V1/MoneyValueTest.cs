using Tinkoff.InvestApi.V1;

namespace Tinkoff.InvestApi.Tests.V1;

public class MoneyValueTest
{
    [Theory]
    [ClassData(typeof(QuotationTestData))]
    public void ConvertToDecimal(Quotation quotation, decimal expectation)
    {
        var moneyValue = new MoneyValue {Units = quotation.Units, Nano = quotation.Nano};
        ((decimal) moneyValue).Should().Be(expectation);
    }
}