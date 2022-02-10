namespace Tinkoff.InvestApi.V1;

public partial class MoneyValue
{
    private const decimal NanoFactor = 1_000_000_000;

    public static implicit operator decimal(MoneyValue value)
    {
        return value.Units + value.Nano / NanoFactor;
    }

    public string Format()
    {
        return $"{(decimal) this} {Currency}";
    }
}