using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.services.Result;

namespace core_service.domain.models;

public class ContributionBankAccount : BankAccount
{
    public DateRange DateRange { get; set; }
    public DateOnly? ActualСlosed { get; set; }
    public UDecimal Amount { get; set; }
    
    public TypeContributionBankAccount TypeContribution { get; set; }

    public PercentContribution Percent { get; set; }

    public ContributionBankAccount(Guid id, Guid userId, string name, string color, Currency currency, Contribution contribution, bool isMaybeNegative, decimal balance = 0)
        : base(id, userId,  name, color, currency, isMaybeNegative, balance, TypeBankAccount.Contribution)
    {
        this.DateRange = contribution.DateRange;
        this.ActualСlosed = contribution.ActualСlosed;
        this.Amount = contribution.Amount;
        this.TypeContribution = contribution.Type;
        this.Percent = contribution.Percent ?? PercentContribution.Empty;
    }
    
    public ContributionBankAccount(Guid userId, string name, string color, Currency currency, Contribution contribution, bool isMaybeNegative, decimal balance = 0)
        : base(userId, name, color, currency, isMaybeNegative, balance, TypeBankAccount.Contribution)
    {
        this.DateRange = contribution.DateRange;
        this.ActualСlosed = contribution.ActualСlosed;
        this.Amount = contribution.Amount;
        this.TypeContribution = contribution.Type;
        this.Percent = contribution.Percent ?? PercentContribution.Empty;
    }
    private ContributionBankAccount(){}
}

public readonly struct Contribution
{
    public DateRange DateRange { get; }
    public DateOnly? ActualСlosed { get; }
    public UDecimal Amount { get; }
    public TypeContributionBankAccount Type { get; }
    public PercentContribution? Percent { get; }
    
    private Contribution(DateRange dateRange, UDecimal amount, TypeContributionBankAccount type, DateOnly? actualClosed = null, PercentContribution? percent = null)
    {
        this.DateRange = dateRange;
        this.Amount = amount;
        this.Type = type;
        this.ActualСlosed = actualClosed;
        this.Percent = percent;
    }

    public static Contribution Create(DateRange dateRange, UDecimal amount, TypeContributionBankAccount type,
        DateOnly? actualClosed = null, PercentContribution? percent = null)
    {
        var res = IsValid(dateRange, amount, type, actualClosed, percent);
        if(res.IsError)
            throw new ArgumentException(res.ErrorMessage);

        return new Contribution(dateRange, amount, type, actualClosed, percent);
    }

    public static Result IsValid(DateRange dateRange, UDecimal amount, TypeContributionBankAccount type,
        DateOnly? actualClosed = null, PercentContribution? percent = null)
    {
        if(actualClosed != null && actualClosed < dateRange.StartDate)
            return Result.Error("Contribution.ActualClosed can't be less than DateRange.StartDate");

        if (type == TypeContributionBankAccount.BankDeposit && percent == null)
            return Result.Error("If Contribution.Type is BankDeposit, percent can't be null");
        
        return Result.Success();
    }
}
