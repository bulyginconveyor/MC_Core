using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOCreditBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Credit";
    
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    [JsonPropertyName("init_payment")]
    public decimal InitPayment { get; set; }
    
    [JsonPropertyName("percent")]
    public decimal? Percent { get; set; }
    
    [JsonPropertyName("unit")]
    public string Unit { get; init; }
    [JsonPropertyName("count_units")]
    public uint CountUnits { get; init; }

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }
    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; set; } = null;

    [JsonPropertyName("loan_object_id")]
    public Guid? LoanObjectId { get; set; }
    [JsonPropertyName("purpose_loan")]
    public string? PurposeLoan { get; set; }

    [JsonPropertyName("type_credit")]
    public string TypeCredit { get; set; }
}
