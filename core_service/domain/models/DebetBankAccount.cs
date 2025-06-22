namespace core_service.domain.models;

public class DebetBankAccount : BankAccount
{
    public DebetBankAccount(Guid id, Guid userId, string name, string color, Currency currency, decimal balance = 0) 
        : base(id, userId, name, color, currency, false, balance){}
    
    public DebetBankAccount(Guid userId, string name, string color, Currency currency, decimal balance = 0) 
        : base(userId, name, color, currency, false, balance){}
    
    private DebetBankAccount(){}
}
