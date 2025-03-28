using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOOperation
{
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    [JsonPropertyName("period_type")]
    public string? PeriodType { get; set; }
    [JsonPropertyName("period_value")]
    public ushort? PeriodValue { get; set; }
    
    [JsonPropertyName("credit_bank_account_id")]
    public Guid? CreditBankAccountId { get; set; }
    [JsonPropertyName("debet_bank_account_id")]
    public Guid? DebetBankAccountId { get; set; }
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; } 
}
