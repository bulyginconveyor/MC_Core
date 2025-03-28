using System.Linq.Expressions;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.services.ExpressionHelpers;

namespace core_service.domain.logic.filters.bank_account.active;

public class ActiveBankAccountFilter : BankAccountFilter<ActiveBankAccount>
{
    public decimal? MinBuyPrice { get; set; } = null;
    public decimal? MaxBuyPrice { get; set; } = null;
    
    public DateTime? StartBuyDate { get; set; } = null;
    public DateTime? EndBuyDate { get; set; } = null;
    public string? TypeActive { get; set; } = null;
    
    public new static BankAccountFilterBuilder<ActiveBankAccount> CreateBuilder => new ActiveBankAccountFilterBuilder();

    public override Expression<Func<ActiveBankAccount, bool>> ToExpression()
    {
        var f = this;

        Expression<Func<ActiveBankAccount, bool>> expression = base.ToExpression();
        
        if(f.MinBuyPrice != null && f.MaxBuyPrice != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterBuyPriceRange());
        else if(f.MinBuyPrice != null && f.MaxBuyPrice == null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterBuyPrice());
        
        if(f.StartBuyDate != null && f.EndBuyDate != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterDateRange());
        else if(f.StartBuyDate != null && f.EndBuyDate == null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterDate());
        
        if(f.TypeActive != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterTypeActive());

        if (expression is null)
            return ab => true;
        
        return expression;
    }
}

public class ActiveBankAccountFilterBuilder : BankAccountFilterBuilder<ActiveBankAccount>
{
    protected override BankAccountFilter<ActiveBankAccount> _filter { get; set; } = new ActiveBankAccountFilter();
    
    public void WithBuyPrice(decimal value) => ((ActiveBankAccountFilter)_filter).MinBuyPrice = value;

    public void WithBuyPriceRange(decimal min, decimal max)
    {
        ((ActiveBankAccountFilter)_filter).MinBuyPrice = min;
        ((ActiveBankAccountFilter)_filter).MaxBuyPrice = max;
    }

    public void WithBuyDate(DateTime value) => ((ActiveBankAccountFilter)_filter).StartBuyDate = value;

    public void WithBuyDateRange(DateTime start, DateTime end)
    {
        ((ActiveBankAccountFilter)_filter).StartBuyDate = start;
        ((ActiveBankAccountFilter)_filter).EndBuyDate = end;
    }
    
    public void WithTypeActive(TypeActiveBankAccount value) => ((ActiveBankAccountFilter)_filter).TypeActive = value.ToString();
}
