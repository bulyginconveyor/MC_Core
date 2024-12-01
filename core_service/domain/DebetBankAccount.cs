using core_service.domain.valueobjects;

namespace core_service.domain;

public class DebetBankAccount : BankAccount
{
    public DebetBankAccount(Guid id, string name, string color, Currency currency, decimal balance = 0) 
        : base(id, name, color, currency, false, balance){}
    
    private DebetBankAccount(){}
}