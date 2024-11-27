namespace core_service.domain.valueobjects;

public record Balance
{
    public decimal Value { get; init; }
    public bool isMaybeNegative { get; init; }

    private Balance(bool isMaybeNegative, decimal value = 0)
    {
        Value = value;
        this.isMaybeNegative = isMaybeNegative;
    }

    public static Balance Create(bool isMaybeNegative, decimal value = 0)
    {
        if(!BalanceParametersValid(isMaybeNegative, value))
            throw new ArgumentException("Invalid balance parameters");

        return new Balance(isMaybeNegative, value);
    }

    private static bool BalanceParametersValid(bool isMaybeNegative, decimal value) => (isMaybeNegative && value <= 0) || value >= 0;

    public bool TryDecrease(decimal value)
    {
        if (isMaybeNegative)
            return true;
        if (Value >= value)
            return true;
        
        return false;
    }
    
}