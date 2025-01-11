using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOContributionBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Contribution";
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public DateTime? ActualClosedDate { get; set; }
    public decimal Amount { get; set; }
    
    public string TypeContribution { get; set; }

    public decimal? Percent { get; set; }
    public ushort? CountDaysForPercent { get; set; }
}
