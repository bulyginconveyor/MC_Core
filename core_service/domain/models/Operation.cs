using core_service.domain.models.@base;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.services.GuidGenerator;
using core_service.services.Result;

namespace core_service.domain.models;

public class Operation : Entity, IDbModel, IByUserModel
{
    public Name Name { get; set; }
    public DateOnly Date { get; set; }
    public UDecimal Amount { get; private set; }
    public Period? Period { get; set; }
    public BankAccount? CreditBankAccount { get; set; }
    public BankAccount? DebetBankAccount { get; set; }
    public Category? Category { get; set; }
    public StatusOperation Status { get; private set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
    
    public Guid UserId { get; set; }
    
    public Operation(Name name, DateOnly date, UDecimal amount, Period? period = null, BankAccount? credit = null,
        BankAccount? debet = null, Category? category = null)
    {
        if (credit != null && !TryCreateOperation(date, amount, credit))
            throw new Exception($"Invalid credit bank account in operation: balance ({credit.Balance.Value}) less than amount ({amount.Value})");

        if (credit != null && debet != null)
            throw new ArgumentException("Can't debet is null and credit is null! Min - one bankAccount is not null!");

        this.Id = GuidGenerator.GenerateByBytes();
        this.Name = name;
        this.Date = date;
        this.Amount = amount;
        this.Period = period;
        this.CreditBankAccount = credit;
        this.DebetBankAccount = debet;
        this.Category = category;
        
        this.Status = Date <= DateOnly.FromDateTime(DateTime.Now) ? StatusOperation.Closed : StatusOperation.Open;
    }
    
    public Operation(Guid Id, Name name, DateOnly date, UDecimal amount, Period? period = null, BankAccount? credit = null,
        BankAccount? debet = null, Category? category = null)
    {
        if (credit != null && !TryCreateOperation(date, amount, credit))
            throw new Exception($"Invalid credit bank account in operation: balance ({credit.Balance.Value}) less than amount ({amount.Value})");

        if (credit != null && debet != null)
            throw new ArgumentException("Can't debet is null and credit is null! Min - one bankAccount is not null!");
    
        this.Id = Id;
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

    public Result TryChangeAmount(UDecimal amount, bool operationWasPreviouslySaved = true)
    {
        if (amount.IsZero)
            return Result.Error("Amount can not be zero");

        if(!operationWasPreviouslySaved)
            if (CreditBankAccount != null && !CreditBankAccount.Balance.TryDecrease(amount))
                return Result.Error("Balance of CreditBankAccount is smaller than amount Operation!");
            else
                return Result.Success();
        
        var diff = (decimal)Amount - (decimal)amount;
        switch (diff)
        {
            case > 0:
            {
                if (DebetBankAccount != null)
                    if (!DebetBankAccount.Balance.TryDecrease(diff))
                        return Result.Error($"Not enough money in debet account!");
                break;
            }
            case < 0:
            {
                if (CreditBankAccount != null)
                    if (!CreditBankAccount.Balance.TryDecrease(-diff))
                        return Result.Error($"Not enough money in credit account!");
                break;
            }
        }

        return Result.Success();
    }

    public Result ChangeAmount(UDecimal amount, bool operationWasPreviouslySaved = true)
    {
        if(TryChangeAmount(amount, operationWasPreviouslySaved).IsError)
            return Result.Error("Can't change amount");

        if (!operationWasPreviouslySaved)
        {
            Amount = amount;
            return Result.Success();
        }

        var diff = (decimal)Amount - (decimal)amount;
        switch (diff)
        {
            case > 0:
            {
                if (DebetBankAccount != null)
                    DebetBankAccount.Balance.Decrease(diff);
                if (CreditBankAccount != null)
                    CreditBankAccount.Balance.Increase(diff);
                break;
            }
            case < 0:
            {
                if (CreditBankAccount != null)
                    CreditBankAccount.Balance.Decrease(-diff);
                if (DebetBankAccount != null)
                    DebetBankAccount.Balance.Increase(-diff);
                break;
            }
        }
        
        return Result.Success();
    }

    public Result Perform()
    {
        if(CreditBankAccount != null)
            CreditBankAccount.Balance.Decrease(Amount);
        if(DebetBankAccount != null)
            DebetBankAccount.Balance.Increase(Amount);
        
        return Result.Success();
    }
}
