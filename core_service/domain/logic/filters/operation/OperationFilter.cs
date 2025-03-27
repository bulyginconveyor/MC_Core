using System.Linq.Expressions;
using core_service.domain.models;
using core_service.services.ExpressionHelpers;

namespace core_service.domain.logic.filters.operation;

public struct OperationFilter()
{
    public string? Name { get; set; } = null;
    public DateOnly? StartDate { get; set; } = null;
    public DateOnly? EndDate { get; set; } = null;
    public decimal? MinAmount { get; set; } = null;
    public decimal? MaxAmount { get; set; } = null;
    
    public Guid? CreditBankAccountId { get; set; } = null;
    public Guid? DebetBankAccountId { get; set; } = null;
    
    public Guid? CategoryId { get; set; } = null;
    
    public static OperationFilterBuilder CreateBuilder() => new();
    
    public Expression<Func<Operation, bool>> ToExpression()
    {
        var filter = this;

        Expression<Func<Operation, bool>>? expression = null;

        if (filter.Name != null)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterName());
        
        if (filter.StartDate != null && filter.EndDate != null)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterDateRange());
        else if(filter.StartDate != null && filter.EndDate == null)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterDate());
        
        if (filter.MinAmount != null && filter.MaxAmount != null)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterAmountRange());
        else if(filter.MinAmount != null && filter.MaxAmount == null)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterAmount());
        
        if(filter.CreditBankAccountId != null && filter.DebetBankAccountId != null && filter.CreditBankAccountId == filter.DebetBankAccountId)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterBankAccount());
        else
        {
            if(filter.CreditBankAccountId != null)
                expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterCreditBankAccount());
            if(filter.DebetBankAccountId != null)
                expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterDebetBankAccount());
        }
        
        if(filter.CategoryId != null)
            expression = expression.ExpressionConcatWithAnd(filter.ExpressionFilterCategory());

        if (expression == null)
            expression = o => true;
            
        return expression;
    }
}
public class OperationFilterBuilder {

    private OperationFilter _filter = new();
    
    public OperationFilter Build() => _filter;

    public void WithName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return;
        _filter.Name = name;
    }

    public void WithDate(DateOnly date) => _filter.StartDate = date;
    public void WithDateRange(DateOnly start, DateOnly end)
    {
        if(start > end)
            return;
        _filter.StartDate = start;
        _filter.EndDate = end;
    }
    
    public void WithAmount(decimal amount) => _filter.MinAmount = amount;
    public void WithAmountRange(decimal min, decimal max)
    { 
        if(min > max)
            return;
        _filter.MinAmount = min;
        _filter.MaxAmount = max;
    }

    public void WithBankAccount(Guid id)
    {
        _filter.CreditBankAccountId = id;
        _filter.DebetBankAccountId = id;
    }
    public void WithDebetBankAccount(Guid id) => _filter.DebetBankAccountId = id;
    public void WithCreditBankAccount(Guid id) => _filter.CreditBankAccountId = id;
    public void WithBankAccounts(Guid credit, Guid debet)
    {
        _filter.CreditBankAccountId = credit;
        _filter.DebetBankAccountId = debet;
    }
    
    public void WithCategory(Guid id) => _filter.CategoryId = id;
}
