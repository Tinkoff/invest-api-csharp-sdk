namespace Tinkoff.InvestApi.V1;

public partial class Quotation
{
    private const decimal NanoFactor = 1_000_000_000;

    public static implicit operator decimal(Quotation value)
    {
        return value.Units + value.Nano / NanoFactor;
    }

    public static implicit operator Quotation(decimal value)
    {
        var wholePart = (long)value;

        return new Quotation
        {
            Units = wholePart,
            Nano = (int)((value - wholePart) * NanoFactor)
        };
    }
}