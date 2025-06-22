using System.Text.Json.Serialization;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using core_service.domain.models.valueobjects.enums;

namespace core_service.application.rest_api.DTO;

public class DTOCreditBankAccount : DTOBankAccount
{
    public new string TypeBankAccount { get; set; } = "Credit";
    
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    [JsonPropertyName("init_payment")]
    public decimal? InitPayment { get; set; }
    
    [JsonPropertyName("percent")]
    public decimal? Percent { get; set; }
    
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
    [JsonPropertyName("count_units")]
    public uint? CountUnits { get; init; }

    [JsonPropertyName("start_date")]
    public DateOnly StartDate { get; set; }
    [JsonPropertyName("end_date")]
    public DateOnly? EndDate { get; set; }

    [JsonPropertyName("loan_object")]
    public DTOActiveBankAccount? LoanObject { get; set; }
    [JsonPropertyName("purpose_loan")]
    public string? PurposeLoan { get; set; }

    [JsonPropertyName("type_credit")]
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
            UDecimal initPayment = dto.InitPayment is not null ? UDecimal.Parse((decimal)dto.InitPayment) : UDecimal.Zero;
            Percent percent = dto.Percent is not null
                ? domain.models.valueobjects.Percent.Create((decimal)dto.Percent)
                : domain.models.valueobjects.Percent.Zero;

            UnitTerm unitTerm = (UnitTerm)Enum.Parse(typeof(UnitTerm), dto.Unit);
            Term term = dto.CountUnits is not null ? Term.Create(unitTerm, (uint)dto.CountUnits) : null;

            TypeCreditBankAccount typeCreditBankAccount =
                (TypeCreditBankAccount)Enum.Parse(typeof(TypeCreditBankAccount), dto.TypeCredit);
            Name purposeLoan = dto.PurposeLoan is null
                ? domain.models.valueobjects.Name.Empty
                : domain.models.valueobjects.Name.Create(dto.PurposeLoan);

            Loan loan = new Loan(amount, initPayment, dto.StartDate, typeCreditBankAccount, 
                dto.LoanObject, purposeLoan, percent, term);


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
