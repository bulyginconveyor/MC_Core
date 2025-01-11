using System.Text.Json.Serialization;

namespace core_service.application.rest_api.DTO;

public class DataDTODebetBankAccount : DataDTOBankAccount
{
    [JsonIgnore]
    public new string TypeBankAccount { get; set; } = "Debet";
}
