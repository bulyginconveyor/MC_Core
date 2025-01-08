namespace core_service.domain.models.valueobjects;

public record Balance
{
    public decimal Value { get; private set; }
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

    private static bool BalanceParametersValid(bool isMaybeNegative, decimal value) => 
        (isMaybeNegative && value <= 0) || value >= 0;

    public bool TryDecrease(decimal value) => isMaybeNegative || Value >= value;

    public void Decrease(UDecimal entityAmount) => Value = Value - entityAmount.Value;
    public void Decrease(decimal entityAmount) => Value = Value - entityAmount;
    public void Decrease(long entityAmount) => Value = Value - entityAmount;

    public void Increase(UDecimal entityAmount) => Value = Value + entityAmount.Value;
    public void Increase(decimal entityAmount) => Value = Value + entityAmount;
    public void Increase(long entityAmount) => Value = Value + entityAmount;
    
    public static implicit operator decimal(Balance balance) => balance.Value;
}
