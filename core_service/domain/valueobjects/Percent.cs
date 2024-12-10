using core_service.services.Result;

namespace core_service.domain.valueobjects;

public record Percent
{
    public decimal Value { get; init; }

    private Percent(decimal value)
    {
        Value = value;
    }


    public static Percent Create(decimal value)
    {
        var res = IsValid(value);
        if(res.IsError)
            throw new ArgumentException(res.ErrorMessage);

        return new Percent(value);
    }
    public static Percent Create(double value) => Percent.Create((decimal)value);
    public static Percent Create(long value) => Percent.Create((decimal)value);
    public static Result IsValid(decimal value)
    {
        return value >= 0 && value <= 100 ? 
            Result.Success() 
            : 
            Result.Error("Percent must be between 0 and 100");
    } 
    
    public static Percent Zero => new Percent(0);
    public bool IsZero => Value == 0;
}