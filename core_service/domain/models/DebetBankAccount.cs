namespace core_service.domain.models;

public class DebetBankAccount : BankAccount
{
    public DebetBankAccount(Guid id, string name, string color, Currency currency, decimal balance = 0) 
        : base(id, name, color, currency, false, balance){}
    
    private DebetBankAccount(){}
}