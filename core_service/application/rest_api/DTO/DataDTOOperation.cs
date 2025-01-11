namespace core_service.application.rest_api.DTO;

public class DataDTOOperation
{
    public Guid? Id { get; set; }
    
    public string? Name { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    
    public string? PeriodType { get; set; }
    public ushort? PeriodValue { get; set; }
    
    public Guid? CreditBankAccountId { get; set; }
    public Guid? DebetBankAccountId { get; set; }
    public Guid? CategoryId { get; set; } 
}
