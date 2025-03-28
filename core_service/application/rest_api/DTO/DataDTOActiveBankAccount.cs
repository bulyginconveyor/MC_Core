using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOActiveBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Active";
    
    [JsonPropertyName("buy_price")]
    public decimal BuyPrice { get; set; }
    [JsonPropertyName("buy_date")]
    public DateTime BuyDate { get; set; }
    [JsonPropertyName("type_active")]
    public string TypeActive { get; set; }
    [JsonPropertyName("photo_url")]
    public string? PhotoUrl { get; set; }
}
