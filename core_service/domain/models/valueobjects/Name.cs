using core_service.services.Result;
using static System.String;

namespace core_service.domain.models.valueobjects;

public record Name
{
    public string? Value { get; init; }

    private Name(string value) => Value = IsNullOrEmpty(value) ? null : value;

    public bool IsEmpty() => this.Value == null;
    public static Name Create(string value)
    {
        var res = NameIsValid(value);

        if (res.IsError)
            throw new ArgumentException(res.ErrorMessage);
        
        return new Name(value);
    }

    public static Name Empty => new(string.Empty);

    public static Result NameIsValid(string value) =>
        IsNullOrWhiteSpace(value) ?
            Result.Error("Invalid name: Name is spaces or empty or null")
            :
            Result.Success();
    
    public virtual bool Equals(Name? other) => Value == other?.Value;
    public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;
}