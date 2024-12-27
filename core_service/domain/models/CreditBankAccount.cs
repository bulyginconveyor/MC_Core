using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;

namespace core_service.domain.models;

public class CreditBankAccount : BankAccount
{
    public UDecimal Amount { get; set; }
    public UDecimal InitPayment { get; set; }
    public Percent Percent { get; set; }
    public Term Term { get; set; }
    public DateRange DateRange { get; set; }
    public ActiveBankAccount? LoanObject { get; set; }
    public Name PurposeLoan { get; set; }
    public TypeCreditBankAccount TypeCredit { get; set; }

    public CreditBankAccount(Guid id, string name, string color, Currency currency, Loan loan, decimal balance = 0)
        : base(id, name, color, currency, true, balance, TypeBankAccount.Credit)
    {
        this.Amount = loan.Amount;
        this.InitPayment = loan.InitPayment;
        this.Percent = loan.Percent;
        this.Term = loan.Term;
        this.DateRange = loan.DateRange;
        this.TypeCredit = loan.Type;
        this.LoanObject = loan.LoanObject;
        this.PurposeLoan = loan.PurposeLoan;
    }
    private CreditBankAccount(){}
}


public readonly struct Loan {
    public Loan(UDecimal amount, UDecimal initPayment, Percent percent, Term term, DateTime startDate, TypeCreditBankAccount typeCreditBankAccount, ActiveBankAccount? loanObject = null, Name? purposeLoan = null)
    {
        this.Amount = amount;
        this.InitPayment = initPayment;
        this.Percent = percent;
        this.Term = term;

        var resEndDate = term.EndDate(startDate);
        if(resEndDate.IsError)
            throw new ArgumentNullException(resEndDate.ErrorMessage);
        
        this.DateRange = DateRange.Create(startDate, resEndDate.Value);
        this.Type = typeCreditBankAccount;
        this.LoanObject = loanObject;
        this.PurposeLoan = purposeLoan ?? Name.Empty;
    }
    public UDecimal Amount { get; }
    public UDecimal InitPayment { get; }
    public Percent Percent { get; }
    public Term Term { get; }
    public DateRange DateRange { get; }
    public ActiveBankAccount? LoanObject { get; } = null;
    public Name PurposeLoan { get; }
    public TypeCreditBankAccount Type { get; }
}


