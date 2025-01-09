using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace core_service.application.rest_controllers.DTO;

public class DTOActiveBankAccount : DTOBankAccount
{
    public new string TypeBankAccount { get; set; } = "Active";
    
    public decimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public string TypeActive { get; set; }
    public string? PhotoUrl { get; set; }

    public static DTOActiveBankAccount? CreateLight(ActiveBankAccount? bankAccount)
    {
        if(bankAccount is null)
            return null;

        return new DTOActiveBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            BuyPrice = bankAccount.BuyPrice.Value,
            BuyDate = bankAccount.BuyDate,
            TypeActive = bankAccount.TypeActive.ToString(),
            PhotoUrl = bankAccount.PhotoUrl.Url
        };
    }
    
    public static implicit operator ActiveBankAccount?(DTOActiveBankAccount? dto)
    {
        if (dto is null)
            return null;

        try
        {
            TypeActiveBankAccount type = (TypeActiveBankAccount)Enum.Parse(typeof(TypeActiveBankAccount), dto.TypeActive);
            UDecimal buyPrice = UDecimal.Parse(dto.BuyPrice);
            PhotoUrl photoUrl = dto.PhotoUrl is null 
                ? domain.models.valueobjects.PhotoUrl.Empty 
                : domain.models.valueobjects.PhotoUrl.Create(dto.PhotoUrl);
            
            Active active = new Active(buyPrice, dto.BuyDate, type, photoUrl);
            
            var bankAccount = new ActiveBankAccount(dto.Id, dto.Name, dto.Color, dto.Currency, active, dto.Balance);
            return bankAccount;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public static implicit operator DTOActiveBankAccount?(ActiveBankAccount? bankAccount)
    {
        if (bankAccount is null)
            return null;

        if (bankAccount.Operations is null || bankAccount.Operations.Count() == 0)
            return CreateLight(bankAccount);
        
        return new DTOActiveBankAccount
        {
            Id = bankAccount.Id,
            Name = bankAccount.Name.Value,
            Color = bankAccount.Color.Value,
            Balance = bankAccount.Balance.Value,
            Currency = bankAccount.Currency,
            TypeBankAccount = bankAccount.Type.ToString(),
            Operations = bankAccount.Operations.Select(x => DTOOperation.CreateLight(x)).ToList(),
            BuyPrice = bankAccount.BuyPrice.Value,
            BuyDate = bankAccount.BuyDate,
            TypeActive = bankAccount.TypeActive.ToString(),
            PhotoUrl = bankAccount.PhotoUrl.Url
        };

    }
}
