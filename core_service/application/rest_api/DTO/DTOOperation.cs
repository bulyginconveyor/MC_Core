using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.domain.models.valueobjects;

namespace core_service.application.rest_controllers.DTO;

public class DTOOperation
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    
    public DTOPeriod? Period { get; set; }
    public DTOBankAccount? CreditBankAccount { get; set; }
    public DTOBankAccount? DebetBankAccount { get; set; }
    public DTOCategory? Category { get; set; }
    
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
