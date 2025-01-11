using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTOCreditBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Credit";
    
    public decimal Amount { get; set; }
    public decimal InitPayment { get; set; }
    
    public decimal? Percent { get; set; }
    
    public string Unit { get; init; }
    public uint CountUnits { get; init; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } = null;

    public Guid? LoanObjectId { get; set; }
    public string? PurposeLoan { get; set; }

    public string TypeCredit { get; set; }
}
