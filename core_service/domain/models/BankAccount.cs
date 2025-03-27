using core_service.domain.models.@base;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.services.Result;
using Color = core_service.domain.models.valueobjects.Color;

namespace core_service.domain.models;

public class BankAccount : Entity, IDbModel, IByUserModel
{
    public Name Name { get; set; } = null!;
    public Color Color { get; set; } = null!;
    public Balance Balance { get; set; } = null!;
    public Currency Currency { get; set; } = null!;
    public TypeBankAccount Type { get; set; }

    public BankAccount(Guid id, string name, string color, Currency currency, bool isMaybeNegative, decimal balance = 0, TypeBankAccount type = TypeBankAccount.Debet)
    {
        this.Id = id;
        this.Name = Name.Create(name);
        this.Color = Color.Parse(color);
        this.Balance = Balance.Create(isMaybeNegative, balance);
        this.Currency = currency;
        this.Type = type;
    }
    
    public BankAccount(string name, string color, Currency currency, bool isMaybeNegative, decimal balance = 0, TypeBankAccount type = TypeBankAccount.Debet)
    {
        this.Id = Guid.NewGuid();
        this.Name = Name.Create(name);
        this.Color = Color.Parse(color);
        this.Balance = Balance.Create(isMaybeNegative, balance);
        this.Currency = currency;
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

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null!;
    public DateTime? DeletedAt { get; } = null!;
    public Guid UserId { get; set; }
}
