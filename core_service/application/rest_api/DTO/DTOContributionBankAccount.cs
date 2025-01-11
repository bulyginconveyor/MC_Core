using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;

namespace core_service.application.rest_api.DTO;

public class DTOContributionBankAccount : DTOBankAccount
{
    public new string TypeBankAccount { get; set; } = "Contribution";
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public DateTime? ActualClosedDate { get; set; }
    public decimal Amount { get; set; }
    
    public string TypeContribution { get; set; }
    
    public decimal? Percent { get; set; }
    public ushort? CountDaysForPercent { get; set; }

    public static DTOContributionBankAccount? CreateLight(ContributionBankAccount contribution)
    {
        if(contribution == null)
            return null;
        
        return new DTOContributionBankAccount
        {
            Id = contribution.Id,
            Name = contribution.Name.Value,
            Color = contribution.Color.Value,
            Balance = contribution.Balance.Value,
            Currency = contribution.Currency,
            TypeBankAccount = contribution.Type.ToString(),
            Operations = null,
            
            StartDate = contribution.DateRange.StartDate,
            EndDate = contribution.DateRange.EndDate,
            ActualClosedDate = contribution.ActualСlosed,
            Amount = contribution.Amount.Value,
            
            TypeContribution = contribution.TypeContribution.ToString(),
            
            Percent = contribution.Percent.Percent,
            CountDaysForPercent = contribution.Percent.CountDays,
        };
    }

    public static implicit operator ContributionBankAccount?(DTOContributionBankAccount? dto)
    {
        if (dto is null)
            return null;

        try
        {
            DateRange dateRange = DateRange.Create(dto.StartDate, dto.EndDate);
            UDecimal amount = UDecimal.Parse(dto.Amount);
            TypeContributionBankAccount typeContribution = (TypeContributionBankAccount)Enum.Parse(typeof(TypeContributionBankAccount), dto.TypeContribution);
            PercentContribution percent = dto.Percent == null && dto.CountDaysForPercent == null 
                ? PercentContribution.Empty  
                : PercentContribution.Create(UDecimal.Parse((decimal)dto.Percent!), (ushort)dto.CountDaysForPercent!);
            
            Contribution contribution = Contribution.Create(dateRange, amount, typeContribution, dto.ActualClosedDate, percent);

            //TODO: Здесь isMaybeNegative стоит false, могут быть ошибки, так как я не помню, почему же вклад может быть с минусовым балансом?!
            var bankAccount =
                new ContributionBankAccount(dto.Id, dto.Name, dto.Color, dto.Currency, contribution, false, dto.Balance);
            
            return bankAccount;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static implicit operator DTOContributionBankAccount?(ContributionBankAccount? bankAccount)
    {
        if (bankAccount is null)
            return null;

        if(bankAccount.Operations is null || bankAccount.Operations.Count() == 0)
            return CreateLight(bankAccount);

        return new DTOContributionBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            Operations = bankAccount.Operations!.Select(x => (DTOOperation)x!).ToList(),

            StartDate = bankAccount.DateRange.StartDate,
            EndDate = bankAccount.DateRange.EndDate,
            ActualClosedDate = bankAccount.ActualСlosed,
            Amount = bankAccount.Amount.Value,

            TypeContribution = bankAccount.TypeContribution.ToString(),

            Percent = bankAccount.Percent.Percent,
            CountDaysForPercent = bankAccount.Percent.CountDays,
        };
    }
}
