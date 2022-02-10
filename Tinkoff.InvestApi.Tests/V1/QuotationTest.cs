using Tinkoff.InvestAPI.V1;

namespace Tinkoff.InvestApi.Tests.V1;

public class QuotationTest
{
    [Theory]
    [ClassData(typeof(QuotationTestData))]
    public void ConvertToDecimal(Quotation quotation, decimal expectation)
    {
        ((decimal) quotation).Should().Be(expectation);
    }
}