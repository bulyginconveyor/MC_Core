using System.Linq.Expressions;
using core_service.domain.models;
using core_service.services.ExpressionHelpers;

namespace core_service.domain.logic.filters.bank_account;

public class BankAccountFilter<T>() where T : BankAccount
{
    public string? Name { get; set; } = null;

    public decimal? MinBalance { get; set; } = null;
    public decimal? MaxBalance { get; set; } = null;
    
    public Guid? CurrencyId { get; set; } = null;
    public string? TypeBankAccount { get; set; } = null;
    
    public static BankAccountFilterBuilder<T> CreateBuilder() => new();

    public virtual Expression<Func<T, bool>> ToExpression()
    {
        var f = this;

        Expression<Func<T, bool>>? expression = null;

        if (f.Name != null)
            expression = expression.ExpressionСoncatWithAnd(f.ExpressionFilterName());
        
        if (f.MinBalance != null && f.MaxBalance != null)
            expression = expression.ExpressionСoncatWithAnd(f.ExpressionFilterBalanceRange());
        else if(f.MinBalance != null && f.MaxBalance == null)
            expression = expression.ExpressionСoncatWithAnd(f.ExpressionFilterBalance());
        
        if (f.CurrencyId != null)
            expression = expression.ExpressionСoncatWithAnd(f.ExpressionFilterCurrencyId());
        
        if (f.TypeBankAccount != null)
            expression = expression.ExpressionСoncatWithAnd(f.ExpressionFilterTypeBankAccount());

        if (expression is null)
            expression = b => true;

        return expression;
    }
}
public class BankAccountFilterBuilder<T> where T : BankAccount
{
    protected virtual BankAccountFilter<T> _filter { get; set; } = new();
    
    public BankAccountFilter<T> Build() => _filter;

    public void WithName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return;
        
        _filter.Name = name;
    }

    public void WithBalance(decimal amount) => _filter.MinBalance = amount;
    public void WithBalanceRange(decimal min, decimal max)
    { 
        if(min > max)
            return;
        
        _filter.MinBalance = min;
        _filter.MaxBalance = max;
    }
    
    public void WithCurrencyId(Guid id) => _filter.CurrencyId = id;
    public void WithTypeBankAccount(string type) => _filter.TypeBankAccount = type;
}
