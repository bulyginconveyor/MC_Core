using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

// Для методов Add и Update
public class DataDTOBankAccount  
{
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("color")]
    public string? Color { get; set; } = null;
    [JsonPropertyName("balance")]
    public decimal Balance { get; set; } = 0;
    [JsonPropertyName("currency_id")]
    public Guid CurrencyId { get; set; }
    [JsonPropertyName("type_bank_account")]
    public string TypeBankAccount { get; set; }
}
