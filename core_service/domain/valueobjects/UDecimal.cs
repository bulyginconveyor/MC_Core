namespace core_service.domain.valueobjects;

public struct UDecimal
{
    public const decimal MinValue = 0;
    public const decimal MaxValue = decimal.MaxValue;
    
    private decimal _value;
    public decimal Value => _value;
    private UDecimal(decimal value) => _value = value;
    
    public static UDecimal Parse(decimal value)
    {
            if(!IsValid(value))
                throw new ArgumentException("Invalid value");

            return new UDecimal(value);
    }
    private static bool IsValid(decimal value) => value >= 0;
    
    public static UDecimal Zero => new UDecimal(0);
    public bool IsZero => Value == 0;
    
}