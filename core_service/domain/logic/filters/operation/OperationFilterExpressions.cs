using System.Linq.Expressions;
using core_service.domain.models;
using core_service.services.ExpressionHelpers;

namespace core_service.domain.logic.filters.operation;

public static class OperationFilterExpressions
{
    internal static Expression<Func<Operation, bool>> ExpressionFilterName(this OperationFilter filter) 
        => o => o.Name.Value.ToLower().Contains(filter.Name!.ToLower());
    
    internal static Expression<Func<Operation, bool>> ExpressionFilterDate(this OperationFilter filter) 
        => o => o.Date == filter.StartDate;
    internal static Expression<Func<Operation, bool>> ExpressionFilterDateRange(this OperationFilter filter)
        => o => o.Date >= filter.StartDate && o.Date <= filter.EndDate;

    internal static Expression<Func<Operation, bool>> ExpressionFilterAmount(this OperationFilter filter) 
        => o => o.Amount.Value == filter.MinAmount;
    internal static Expression<Func<Operation, bool>> ExpressionFilterAmountRange(this OperationFilter filter)
        => o => o.Amount.Value >= filter.MinAmount && o.Amount.Value <= filter.MaxAmount;

    internal static Expression<Func<Operation, bool>> ExpressionFilterBankAccount(this OperationFilter filter)
        => filter.ExpressionFilterCreditBankAccount().ExpressionConcatWithOr(filter.ExpressionFilterDebetBankAccount());
    
    internal static Expression<Func<Operation, bool>> ExpressionFilterCreditBankAccount(this OperationFilter filter)
        => o => o.CreditBankAccount != null && o.CreditBankAccount.Id == filter.CreditBankAccountId;
    internal static Expression<Func<Operation, bool>> ExpressionFilterDebetBankAccount(this OperationFilter filter)
        => o => o.DebetBankAccount != null && o.DebetBankAccount.Id == filter.DebetBankAccountId;
    
    internal static Expression<Func<Operation, bool>> ExpressionFilterCategory(this OperationFilter filter)
        => o => o.Category != null && o.Category.Id == filter.CategoryId;
}
