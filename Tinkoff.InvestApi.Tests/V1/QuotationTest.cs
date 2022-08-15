using Tinkoff.InvestApi.V1;

namespace Tinkoff.InvestApi.Tests.V1;

public class QuotationTest
{
    [Theory]
    [ClassData(typeof(QuotationTestData))]
    public void ConversionTests(Quotation quotation, decimal expectation)
    {
        ((decimal) quotation).Should().Be(expectation);

        quotation = expectation;
        ((decimal) quotation).Should().Be(expectation);
    }
}