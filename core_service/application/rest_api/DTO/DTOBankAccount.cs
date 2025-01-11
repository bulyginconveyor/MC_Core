using core_service.domain.models;
using core_service.domain.models.enums;

namespace core_service.application.rest_api.DTO;

public class DTOBankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal Balance { get; set; }
    public DTOCurrency Currency { get; set; }
    public string TypeBankAccount { get; set; }
    public List<DTOOperation>? Operations { get; set; }

    public static DTOBankAccount? CreateLight(BankAccount? bankAccount)
    {
        if (bankAccount is null)
            return null;

        return new DTOBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            Operations = null
        };
    }
    
    public static implicit operator BankAccount?(DTOBankAccount? dto)
    {
        if (dto is null)
            return null;

        TypeBankAccount type = (TypeBankAccount)Enum.Parse(typeof(TypeBankAccount), dto.TypeBankAccount);
        bool isMaybeNegative = type == domain.models.enums.TypeBankAccount.Credit;
        domain.models.valueobjects.Balance balance =
            domain.models.valueobjects.Balance.Create(isMaybeNegative, dto.Balance);
        try
        {
            var bankAccount = new BankAccount(dto.Id, dto.Name, dto.Color, dto.Currency, isMaybeNegative, dto.Balance,
                type);
            return bankAccount;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public static implicit operator DTOBankAccount?(BankAccount? bankAccount)
    {
        if (bankAccount is null)
            return null;

        if (bankAccount.Operations is null || bankAccount.Operations.Count() == 0)
            return CreateLight(bankAccount);
        
        return new DTOBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            Operations = bankAccount.Operations.Select(x => DTOOperation.CreateLight(x)).ToList()
        };

    }
}
