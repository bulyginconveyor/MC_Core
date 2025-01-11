namespace core_service.application.rest_api.DTO;

// Для методов Add и Update
public class DataDTOBankAccount  
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; } = null;
    public decimal Balance { get; set; } = 0;
    public Guid CurrencyId { get; set; }
    public string TypeBankAccount { get; set; }
}
