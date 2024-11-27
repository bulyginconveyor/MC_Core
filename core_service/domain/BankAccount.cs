using core_service.domain.@base;
using core_service.domain.enums;
using core_service.domain.valueobjects;
using core_service.services.Result;
using Color = core_service.domain.valueobjects.Color;

namespace core_service.domain;

public class BankAccount : Entity, IDbModel
{
    public Name Name { get; set; } = null!;
    public Color Color { get; set; } = null!;
    public Balance Balance { get; set; } = null!;
    public Currency Currency { get; set; } = null!;
    public TypeBankAccount Type { get; set; }

    public BankAccount(Guid id, string name, string color, Currency currency, bool isMaybeNegative, decimal balance = 0, TypeBankAccount type = TypeBankAccount.Debet)
    {
        this.Name = Name.Create(name);
        this.Color = Color.Parse(color);
        this.Balance = Balance.Create(isMaybeNegative, balance);
        this.Currency = currency;
        this.Id = id;
        this.Type = type;
    }

    protected BankAccount()
    { }

    public IEnumerable<Operation> Operations { get; private set; } = null!;

    public Result SetOperations(IEnumerable<Operation> operations)
    {
        var enumerable = operations.ToList();
        if (enumerable.Count == 0)
            return Result.Error("Operations is empty!");
        
        this.Operations = enumerable;
        return Result.Success();
    }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; } = null!;
    public DateTime? DeletedAt { get; } = null!;
}
