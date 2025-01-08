using core_service.domain.models;
using core_service.domain.models.enums;

namespace core_service.application.rest_controllers.DTO;

public class DTODebetBankAccount : DTOBankAccount
{
    public static implicit operator DebetBankAccount?(DTODebetBankAccount? dto)
    {
        if (dto is null)
            return null;

        try
        {
            var bankAccount = new DebetBankAccount(dto.Id, dto.Name, dto.Color, dto.Currency, dto.Balance);
            return bankAccount;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public static implicit operator DTODebetBankAccount?(DebetBankAccount? bankAccount)
    {
        if (bankAccount is null)
            return null;

        if (bankAccount.Operations is null || bankAccount.Operations.Count() == 0)
            return (DTODebetBankAccount)CreateLight(bankAccount);
        
        return new DTODebetBankAccount
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
