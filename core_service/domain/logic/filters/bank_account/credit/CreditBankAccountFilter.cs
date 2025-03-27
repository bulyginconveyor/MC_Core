using System.Linq.Expressions;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.services.ExpressionHelpers;

namespace core_service.domain.logic.filters.bank_account.credit;

public class CreditBankAccountFilter : BankAccountFilter<CreditBankAccount>
{
    public decimal? MinAmount { get; set; } = null;
    public decimal? MaxAmount { get; set; } = null;
    
    public decimal? MinInitPayment { get; set; } = null;
    public decimal? MaxInitPayment { get; set; } = null;
    
    public decimal? MinPercent { get; set; } = null;
    public decimal? MaxPercent { get; set; } = null;
    
    public DateTime? StartDateRange { get; set; } = null;
    public DateTime? EndDateRange { get; set; } = null;
    
    public string? TypeCredit { get; set; } = null;

    public override Expression<Func<CreditBankAccount, bool>> ToExpression()
    {
        var f = this;
        Expression<Func<CreditBankAccount, bool>>? expression = base.ToExpression();

        if (f.MinAmount.HasValue && f.MaxAmount.HasValue)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterAmountRange());
        else if (f.MinAmount.HasValue && !f.MaxAmount.HasValue)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterAmount());
        
        if (f.MinInitPayment.HasValue && f.MaxInitPayment.HasValue)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterInitPaymentRange());
        else if (f.MinInitPayment.HasValue && !f.MaxInitPayment.HasValue)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterInitPayment());
        
        if (f.MinPercent.HasValue && f.MaxPercent.HasValue)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterPercentRange());
        else if (f.MinPercent.HasValue && !f.MaxPercent.HasValue)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterPercent());
        
        if (f.StartDateRange != null && f.EndDateRange != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterDateRange());
        
        if (f.TypeCredit != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterTypeCredit());
        
        if (expression == null)
            expression = b => true;
        
        return expression;
    }
}

public class CreditBankAccountFilterBuilder : BankAccountFilterBuilder<CreditBankAccount>
{
    protected override BankAccountFilter<CreditBankAccount> _filter { get; set; } = new CreditBankAccountFilter();

    public void WithAmount(decimal value)
    {
        var f = _filter as CreditBankAccountFilter;
        
        f.MinAmount = value;
    }

    public void WithAmountRange(decimal min, decimal max)
    {
        if (max < min)
            return;
        
        var f = _filter as CreditBankAccountFilter;
        
        f.MinAmount = min;
        f.MaxAmount = max;
    }

    public void WithInitPayment(decimal value)
    {
        var f = _filter as CreditBankAccountFilter;
        
        f.MinInitPayment = value;
    }

    public void WithInitPaymentRange(decimal min, decimal max)
    {
        if (max < min)
            return;
        
        var f = _filter as CreditBankAccountFilter;
        
        f.MinInitPayment = min;
        f.MaxInitPayment = max;
    }

    public void WithPercent(decimal value)
    {
        var f = _filter as CreditBankAccountFilter;
        
        f.MinPercent = value;
    }

    public void WithPercentRange(decimal min, decimal max)
    {
        if (max < min)
            return;
        
        var f = _filter as CreditBankAccountFilter;
        
        f.MinPercent = min;
        f.MaxPercent = max;
    }

    public void WithDateRange(DateTime startDate, DateTime endDate)
    {
        if(startDate > endDate)
            return;
        
        var f = _filter as CreditBankAccountFilter;
        
        f.StartDateRange = startDate;
        f.EndDateRange = endDate;
    }

    public void WithTypeCredit(TypeCreditBankAccount value)
    {
        var f = _filter as CreditBankAccountFilter;
        
        f.TypeCredit = value.ToString();
    }
    
}
