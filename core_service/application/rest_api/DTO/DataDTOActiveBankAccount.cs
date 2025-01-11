using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOActiveBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Active";
    
    public decimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public string TypeActive { get; set; }
    public string? PhotoUrl { get; set; }
}
