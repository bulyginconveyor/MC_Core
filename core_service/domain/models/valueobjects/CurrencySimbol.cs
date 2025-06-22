using core_service.services.Result;
using static System.String;

namespace core_service.domain.models.valueobjects;

public record CurrencySimbol
{
    public string Value { get; init; }

    private CurrencySimbol(string value) => Value = IsNullOrEmpty(value) ? null : value;

    public bool IsEmpty() => this.Value == null;
    public static CurrencySimbol Create(string value)
    {
        var res = SimbolIsValid(value);

        if (res.IsError)
            throw new ArgumentException(res.ErrorMessage);
        
        return new CurrencySimbol(value);
    }

    public static CurrencySimbol Empty => new(string.Empty);

    public static Result SimbolIsValid(string value) =>
        IsNullOrWhiteSpace(value) ?
            Result.Error("Invalid name: Name is spaces or empty or null")
            :
            Result.Success();
    
    public virtual bool Equals(Name? other) => Value == other?.Value;
    public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

    public bool Contains(string subString) => Value?.ToLower().Contains(subString.ToLower()) ?? false;
}
