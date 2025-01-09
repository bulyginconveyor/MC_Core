using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;

namespace core_service.application.rest_controllers.DTO;

public class DTOCreditBankAccount : DTOBankAccount
{
    public new string TypeBankAccount { get; set; } = "Credit";
    
    public decimal Amount { get; set; }
    public decimal InitPayment { get; set; }
    
    public decimal Percent { get; set; }
    
    public string Unit { get; init; }
    public uint CountUnits { get; init; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public DTOActiveBankAccount? LoanObject { get; set; }
    public string? PurposeLoan { get; set; }

    public string TypeCredit { get; set; }

    public static DTOCreditBankAccount? CreateLight(CreditBankAccount? bankAccount)
    {
        if(bankAccount == null)
            return null;
        
        return new DTOCreditBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            Operations = null,
            
            Amount = bankAccount.Amount.Value,
            InitPayment = bankAccount.InitPayment.Value,
            Percent = bankAccount.Percent.Value,
            
            Unit = bankAccount.Term.Unit.ToString(),
            CountUnits = bankAccount.Term.CountUnits,
            
            StartDate = bankAccount.DateRange.StartDate,
            EndDate = bankAccount.DateRange.EndDate,
            
            LoanObject = DTOActiveBankAccount.CreateLight(bankAccount.LoanObject),
            PurposeLoan = bankAccount.PurposeLoan.Value,
            
            TypeCredit = bankAccount.TypeCredit.ToString()
        };
    }

    public static implicit operator CreditBankAccount?(DTOCreditBankAccount? dto)
    {
        if(dto == null)
            return null;

        try
        {
            UDecimal amount = UDecimal.Parse(dto.Amount);
            UDecimal initPayment = UDecimal.Parse(dto.InitPayment);
            Percent percent = domain.models.valueobjects.Percent.Create(dto.Percent);

            UnitTerm unitTerm = (UnitTerm)Enum.Parse(typeof(UnitTerm), dto.Unit);
            Term term = Term.Create(unitTerm, dto.CountUnits);

            TypeCreditBankAccount typeCreditBankAccount =
                (TypeCreditBankAccount)Enum.Parse(typeof(TypeCreditBankAccount), dto.TypeCredit);
            Name purposeLoan = dto.PurposeLoan is null
                ? domain.models.valueobjects.Name.Empty
                : domain.models.valueobjects.Name.Create(dto.PurposeLoan);

            Loan loan = new Loan(amount, initPayment, percent, term, dto.StartDate, typeCreditBankAccount, 
                dto.LoanObject, purposeLoan);


            return new CreditBankAccount(dto.Id, dto.Name, dto.Color, dto.Currency, loan, dto.Balance);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static implicit operator DTOCreditBankAccount?(CreditBankAccount? bankAccount)
    {
        if(bankAccount == null)
            return null;

        if (bankAccount.Operations is null || bankAccount.Operations.Count() == 0)
            return CreateLight(bankAccount);

        return new DTOCreditBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            Operations = bankAccount.Operations!.Select(x => (DTOOperation)x!).ToList(),

            Amount = bankAccount.Amount.Value,
            InitPayment = bankAccount.InitPayment.Value,
            Percent = bankAccount.Percent.Value,

            Unit = bankAccount.Term.Unit.ToString(),
            CountUnits = bankAccount.Term.CountUnits,

            StartDate = bankAccount.DateRange.StartDate,
            EndDate = bankAccount.DateRange.EndDate,

            LoanObject = DTOActiveBankAccount.CreateLight(bankAccount.LoanObject),
            PurposeLoan = bankAccount.PurposeLoan.Value,

            TypeCredit = bankAccount.TypeCredit.ToString()
        };
    }
}
