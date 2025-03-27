using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOContributionBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Contribution";
    
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }
    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }
    
    [JsonPropertyName("actual_closed_date")]
    public DateTime? ActualClosedDate { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    
    [JsonPropertyName("type_contribution")]
    public string TypeContribution { get; set; }

    [JsonPropertyName("percent")]
    public decimal? Percent { get; set; }
    [JsonPropertyName("count_days_for_percent")]
    public ushort? CountDaysForPercent { get; set; }
}
