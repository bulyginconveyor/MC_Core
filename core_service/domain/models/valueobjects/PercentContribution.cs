using core_service.services.Result;

namespace core_service.domain.models.valueobjects;

public record PercentContribution
{
    public UDecimal Percent { get; init; }
    public ushort CountDays { get; init; }

    private PercentContribution(UDecimal percent, ushort countDays)
    {
        this.Percent = percent;
        this.CountDays = countDays;
    }
    private PercentContribution(){}

    public static PercentContribution Create(UDecimal percent, ushort countDays)
    {
        var res = IsValid(percent, countDays);
        if(res.IsError)
            throw new ArgumentException(res.ErrorMessage);
        
        return new PercentContribution(percent, countDays);
    }

    public static Result IsValid(UDecimal percent, ushort countDays)
    {
        var message = "";
        if (countDays == 0)
            message = message + "CountDays cannot be 0";
        if (percent.IsZero)
            message = message + " Percent cannot be 0";

        return string.IsNullOrEmpty(message) ? 
            Result.Success()
            : 
            Result.Error(message);
    }

    public virtual bool Equals(PercentContribution? other) =>
        this.CountDays == other?.CountDays && this.Percent.Value == other.Percent.Value;
    public override int GetHashCode() => HashCode.Combine(Percent.Value, CountDays);

    public static PercentContribution Empty => new PercentContribution(UDecimal.Zero, 0);
}
