namespace core_service.domain.models.valueobjects;

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
    public static UDecimal Parse(double value)
    {
        if(!IsValid((decimal)value))
            throw new ArgumentException("Invalid value");

        return new UDecimal((decimal)value);
    }
    public static UDecimal Parse(long value)
    {
        if(!IsValid(value))
            throw new ArgumentException("Invalid value");

        return new UDecimal(value);
    }
    private static bool IsValid(decimal value) => value >= 0;
    
    public static UDecimal Zero => new UDecimal(0);
    public bool IsZero => Value == 0;
    
    
    public static UDecimal operator +(UDecimal a, UDecimal b) => new UDecimal(a.Value + b.Value);
    public static UDecimal operator -(UDecimal a, UDecimal b)
    {
        if(a.Value < b.Value)
            throw new ArgumentException("Invalid value");

        return new UDecimal(a.Value - b.Value);
    }
    public static UDecimal operator *(UDecimal a, UDecimal b) => new UDecimal(a.Value * b.Value);

    public static UDecimal operator /(UDecimal a, UDecimal b)
    {
        if (b.IsZero)
            throw new DivideByZeroException("Cannot divide by zero!");
        return new UDecimal(a.Value / b.Value);
    }

    public static UDecimal operator %(UDecimal a, UDecimal b)
    {
        if (b.IsZero)
            throw new DivideByZeroException("Cannot divide by zero!");
        return new UDecimal(a.Value % b.Value);
    }
    public static UDecimal operator ++(UDecimal a) => new UDecimal(a.Value + 1);
    public static UDecimal operator --(UDecimal a) => a.IsZero ? throw new ArgumentException("Cannot decrement zero!") : new UDecimal(a.Value - 1);
    
    public static implicit operator decimal(UDecimal a) => a.Value;
}