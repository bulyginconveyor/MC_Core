using core_service.domain.@base;
using core_service.domain.enums;
using core_service.domain.valueobjects;

namespace core_service.domain;

public class Operation : Entity, IDbModel
{
    public Name Name { get; set; }
    public DateOnly Date { get; set; }
    public UDecimal Amount { get; set; }
    public Period? Period { get; set; }
    public BankAccount? CreditBankAccount { get; set; }
    public BankAccount? DebetBankAccount { get; set; }
    public Category? Category { get; set; }
    public StatusOperation Status { get; private set; } 

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public DateTime? DeletedAt { get; }
    
    public Operation(Name name, DateOnly date, UDecimal amount, Period? period = null, BankAccount? credit = null,
        BankAccount? debet = null, Category? category = null)
    {
        if (credit != null && !TryCreateOperation(date, amount, credit))
            throw new Exception($"Invalid credit bank account in operation: balance ({credit.Balance.Value}) less than amount ({amount.Value})");
        
        this.Name = name;
        this.Date = date;
        this.Amount = amount;
        this.Period = period;
        this.CreditBankAccount = credit;
        this.DebetBankAccount = debet;
        this.Category = category;
        
        this.Status = Date <= DateOnly.FromDateTime(DateTime.Now) ? StatusOperation.Closed : StatusOperation.Open;
    }
    
    private Operation(){}

    public static bool TryCreateOperation(DateOnly date, UDecimal amount, BankAccount credit) => 
        date <= DateOnly.FromDateTime(DateTime.Now) && credit.Balance.TryDecrease(amount.Value);
    
    public bool IsOpen => Status == StatusOperation.Open;
    public bool IsClosed => Status == StatusOperation.Closed;

    public void Close() => Status = StatusOperation.Closed;
    public void Open() => Status = StatusOperation.Open;
   
}