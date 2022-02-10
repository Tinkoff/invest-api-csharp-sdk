using Tinkoff.InvestApi.V1;

namespace Tinkoff.InvestApi.Tests.V1;

public class QuotationTestData : TheoryData<Quotation, decimal>
{
    public QuotationTestData()
    {
        Add(new Quotation {Units = 12345, Nano = 678900000}, 12345.6789M);
        Add(new Quotation {Units = -12345, Nano = -678900000}, -12345.6789M);
        Add(new Quotation {Units = 0, Nano = 1}, 0.000000001M);
        Add(new Quotation {Units = -0, Nano = -1}, -0.000000001M);
        Add(new Quotation {Units = long.MaxValue, Nano = 1}, 9223372036854775807.000000001M);
    }
}