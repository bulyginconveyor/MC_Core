using System.Linq.Expressions;
using core_service.domain.models;
using core_service.domain.models.enums;
using core_service.services.ExpressionHelpers;

namespace core_service.domain.logic.filters.bank_account.contribution;

public class ContributionBankAccountFilter : BankAccountFilter<ContributionBankAccount>
{
    public DateTime? StartDateRange { get; set; } = null;
    public DateTime? EndDateRange { get; set; } = null;
    
    public decimal? MinAmount { get; set; } = null;
    public decimal? MaxAmount { get; set; } = null;
    
    public string? TypeContribution { get; set; } = null;
    
    public decimal? MinPercent { get; set; } = null;
    public decimal? MaxPercent { get; set; } = null;
    
    public ushort? MinCountDaysForPercent { get; set; } = null;
    public ushort? MaxCountDaysForPercent { get; set; } = null;
    
    public override Expression<Func<ContributionBankAccount, bool>> ToExpression()
    {
        var f = this;

        Expression<Func<ContributionBankAccount, bool>>? expression = null;

        if (f.StartDateRange != null && f.EndDateRange != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterDateRange());
        
        if (f.MinAmount != null && f.MaxAmount != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterAmountRange());
        else if (f.MinAmount != null && f.MaxAmount == null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterAmount());
        
        if (f.TypeContribution != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterTypeContribution());
        
        if (f.MinPercent != null && f.MaxPercent != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterPercentRange());
        else if (f.MinPercent != null && f.MaxPercent == null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterPercent());
        
        if (f.MinCountDaysForPercent != null && f.MaxCountDaysForPercent != null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterCountDaysForPercentRange());
        else if (f.MinCountDaysForPercent != null && f.MaxCountDaysForPercent == null)
            expression = expression.ExpressionConcatWithAnd(f.ExpressionFilterCountDaysForPercent());

        if (expression != null)
            expression = b => true;

        return expression;
    }
}

public class ContributionBankAccountFilterBuilder : BankAccountFilterBuilder<ContributionBankAccount>
{
    protected override BankAccountFilter<ContributionBankAccount> _filter { get; set; } = new ContributionBankAccountFilter();
    
    public void WithDateRange(DateTime? start, DateTime? end)
    {
        if (start == null || end == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.StartDateRange = start;
        f.EndDateRange = end;
    }

    public void WithAmount(decimal? value)
    {
        if (value == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.MinAmount = value;
    }
    public void WithAmountRange(decimal? min, decimal? max)
    {
        if (min == null || max == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.MinAmount = min;
        f.MaxAmount = max;
    }

    public void WithTypeContribution(TypeContributionBankAccount type)
    {
        var f = _filter as ContributionBankAccountFilter;
        
        f.TypeContribution = type.ToString();
    }

    public void WithPercent(decimal? value)
    {
        if (value == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.MinPercent = value;
    }
    public void WithPercentRange(decimal? min, decimal? max)
    {
        if (min == null || max == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.MinPercent = min;
        f.MaxPercent = max;
    }

    public void WithCountDaysForPercent(int? value)
    {
        if (value == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.MinCountDaysForPercent = (ushort)value;
    }
    public void WithCountDaysForPercentRange(int? min, int? max)
    {
        if (min == null || max == null)
            return;
        
        var f = _filter as ContributionBankAccountFilter;
        
        f.MinCountDaysForPercent = (ushort)min;
        f.MaxCountDaysForPercent = (ushort)max;
    }
}
