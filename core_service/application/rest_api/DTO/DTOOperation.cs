using System.Text.Json.Serialization;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;

namespace core_service.application.rest_api.DTO;

public class DTOOperation
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    [JsonPropertyName("period")]
    public DTOPeriod? Period { get; set; }
    [JsonPropertyName("credit_bank_account")]
    public DTOBankAccount? CreditBankAccount { get; set; }
    [JsonPropertyName("debet_bank_account")]
    public DTOBankAccount? DebetBankAccount { get; set; }
    [JsonPropertyName("category")]
    public DTOCategory? Category { get; set; }
    
    [JsonPropertyName("status")]
    public bool Status { get; set; }
    
    public static DTOOperation? CreateLight(Operation? operation)
    {
        if (operation is null)
            return null;

        return new DTOOperation
        {
            Id = operation.Id,
            Name = operation.Name.Value,
            Date = operation.Date,
            Amount = operation.Amount.Value,
            Period = DTOPeriod.CreateLight(operation.Period),
            CreditBankAccount = DTOBankAccount.CreateLight(operation.CreditBankAccount),
            DebetBankAccount = DTOBankAccount.CreateLight(operation.DebetBankAccount),
            Category = DTOCategory.CreateLight(operation.Category),
            Status = operation.Status == StatusOperation.Closed
        };

    }
    
    public static implicit operator Operation?(DTOOperation? dto)
    {
        if (dto is null)
            return null;
        
        Name name = domain.models.valueobjects.Name.Create(dto.Name);
        UDecimal amount = domain.models.valueobjects.UDecimal.Parse(dto.Amount);
        var operation = new Operation(
            name, 
            dto.Date, 
            amount, 
            dto.Period, 
            dto.CreditBankAccount, 
            dto.DebetBankAccount,
            dto.Category);
        
        return operation;
    }
    
    public static implicit operator DTOOperation?(Operation? operation)
    {
        if (operation is null)
            return null;

        return new DTOOperation
        {
            Id = operation.Id,
            Name = operation.Name.Value,
            Date = operation.Date,
            Amount = operation.Amount.Value,
            Period = DTOPeriod.CreateLight(operation.Period),
            CreditBankAccount = DTOBankAccount.CreateLight(operation.CreditBankAccount),
            DebetBankAccount = DTOBankAccount.CreateLight(operation.DebetBankAccount),
            Category = DTOCategory.CreateLight(operation.Category),
            Status = operation.Status == StatusOperation.Closed
        };
    }
}
