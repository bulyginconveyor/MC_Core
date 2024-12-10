using core_service.domain.enums;
using core_service.domain.valueobjects;
using core_service.services.GuidGenerator;

namespace core_service.domain;

public class ActiveBankAccount : BankAccount
{
    public UDecimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public TypeActiveBankAccount TypeActive { get; set; }
    public PhotoUrl PhotoUrl { get; set; }
    
    public ActiveBankAccount(Guid id, string name, string color, Currency currency, Active active, decimal balance = 0) 
        : base(id, name, color, currency, false, balance, TypeBankAccount.Active)
    {
        this.BuyPrice = active.BuyPrice;
        this.BuyDate = active.BuyDate;
        this.TypeActive = active.Type;
        this.PhotoUrl = active.PhotoUrl;
    }
    
    public ActiveBankAccount(string name, string color, Currency currency, Active active, decimal balance = 0) 
        : base(GuidGenerator.GenerateByBytes(), name, color, currency, false, balance, TypeBankAccount.Active)
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
    public DateTime BuyDate { get; }
    public TypeActiveBankAccount Type { get; }
    public PhotoUrl PhotoUrl{ get; }

    public Active(UDecimal buyPrice, DateTime buyDate, TypeActiveBankAccount type, PhotoUrl? photoUrl = null)
    {
        BuyPrice = buyPrice;
        BuyDate = buyDate;
        Type = type;
        PhotoUrl = photoUrl ?? PhotoUrl.Empty;
    }
}

