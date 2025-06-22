using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;

namespace core_service.domain.models;

public class ActiveBankAccount : BankAccount
{
    public UDecimal BuyPrice { get; set; }
    public DateOnly BuyDate { get; set; }
    public TypeActiveBankAccount TypeActive { get; set; }
    public PhotoUrl PhotoUrl { get; set; }
    
    public ActiveBankAccount(Guid id, Guid userId, string name, string color, Currency currency, Active active, decimal balance = 0) 
        : base(id, userId, name, color, currency, false, balance, TypeBankAccount.Active)
    {
        this.BuyPrice = active.BuyPrice;
        this.BuyDate = active.BuyDate;
        this.TypeActive = active.Type;
        this.PhotoUrl = active.PhotoUrl;
    }
    
    public ActiveBankAccount(Guid userId, string name, string color, Currency currency, Active active, decimal balance = 0) 
        : base(userId, name, color, currency, false, balance, TypeBankAccount.Active)
    {
        this.BuyPrice = active.BuyPrice;
        this.BuyDate = active.BuyDate;
        this.TypeActive = active.Type;
        this.PhotoUrl = active.PhotoUrl;
    }

    private ActiveBankAccount(){}
}

public readonly struct Active
{
    public UDecimal BuyPrice { get; }
    public DateOnly BuyDate { get; }
    public TypeActiveBankAccount Type { get; }
    public PhotoUrl PhotoUrl{ get; }

    public Active(UDecimal buyPrice, DateOnly buyDate, TypeActiveBankAccount type, PhotoUrl? photoUrl = null)
    {
        BuyPrice = buyPrice;
        BuyDate = buyDate;
        Type = type;
        PhotoUrl = photoUrl ?? PhotoUrl.Empty;
    }
}

