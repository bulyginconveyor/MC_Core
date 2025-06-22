using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;

namespace core_service.domain.models;

public class CreditBankAccount : BankAccount
{
    public UDecimal Amount { get; set; }
    public UDecimal InitPayment { get; set; } = UDecimal.Zero; //TODO: Сделать необязательным (по умолчанию равен 0)
    public Percent Percent { get; set; } //TODO: Сделать необязательным
    public Term Term { get; set; } //TODO: Сделать необязательным
    public DateRange DateRange { get; set; }
    public ActiveBankAccount? LoanObject { get; set; }
    public Name PurposeLoan { get; set; }
    public TypeCreditBankAccount TypeCredit { get; set; }

    public CreditBankAccount(Guid id, Guid userId, string name, string color, Currency currency, Loan loan, decimal balance = 0)
        : base(id, userId, name, color, currency, true, balance, TypeBankAccount.Credit)
    {
        this.Amount = loan.Amount;
        this.InitPayment = loan.InitPayment;
        this.Percent = loan.Percent ?? Percent.Zero;
        this.Term = loan.Term ?? valueobjects.Term.Null;
        this.DateRange = loan.DateRange;
        this.TypeCredit = loan.Type;
        this.LoanObject = loan.LoanObject;
        this.PurposeLoan = loan.PurposeLoan;
    }
    public CreditBankAccount(Guid userId, string name, string color, Currency currency, Loan loan, decimal balance = 0)
        : base(userId, name, color, currency, true, balance, TypeBankAccount.Credit)
    {
        this.Amount = loan.Amount;
        this.InitPayment = loan.InitPayment;
        this.Percent = loan.Percent ?? Percent.Zero;
        this.Term = loan.Term ?? valueobjects.Term.Null;
        this.DateRange = loan.DateRange;
        this.TypeCredit = loan.Type;
        this.LoanObject = loan.LoanObject;
        this.PurposeLoan = loan.PurposeLoan;
    }
    
    private CreditBankAccount(){}
}


public readonly struct Loan {
    public Loan(UDecimal amount, UDecimal initPayment, DateOnly startDate, TypeCreditBankAccount typeCreditBankAccount, ActiveBankAccount? loanObject = null, Name? purposeLoan = null, Percent? percent = null, Term? term = null)
    {
        this.Amount = amount;
        this.InitPayment = initPayment;
        this.Percent = percent;
        this.Term = term;

        if (term is not null)
        {
            var resEndDate = term.EndDate(startDate);
            if(resEndDate.IsError)
                throw new ArgumentNullException(resEndDate.ErrorMessage);
            this.DateRange = DateRange.Create(startDate, resEndDate.Value);
        }
        else
            this.DateRange = DateRange.Create(startDate, null);
        
        this.Type = typeCreditBankAccount;
        this.LoanObject = loanObject;
        this.PurposeLoan = purposeLoan ?? Name.Empty;
    }
    public UDecimal Amount { get; }
    public UDecimal InitPayment { get; }
    public Percent? Percent { get; }
    public Term? Term { get; }
    public DateRange DateRange { get; }
    public ActiveBankAccount? LoanObject { get; } = null;
    public Name PurposeLoan { get; }
    public TypeCreditBankAccount Type { get; }
}


