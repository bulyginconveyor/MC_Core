using System.Linq.Expressions;
using core_service.domain.logic.filters.bank_account.contribution;
using core_service.domain.models;

namespace core_service.domain.logic.filters.bank_account.credit;

public static class CreditBankAccountFilterExpressions
{
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterAmount(this CreditBankAccountFilter filter)
        => b => b.Amount.Value == filter.MinAmount;
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterAmountRange(this CreditBankAccountFilter filter)
        => b => b.Amount.Value >= filter.MinAmount && b.Amount.Value <= filter.MaxAmount;
    
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterInitPayment(this CreditBankAccountFilter filter)
        => b => b.InitPayment.Value == filter.MinInitPayment;
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterInitPaymentRange(this CreditBankAccountFilter filter)
        => b => b.InitPayment.Value >= filter.MinInitPayment && b.InitPayment.Value <= filter.MaxInitPayment;
    
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterPercent(this CreditBankAccountFilter filter)
        => b => b.Percent.Value == filter.MinPercent;
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterPercentRange(this CreditBankAccountFilter filter)
        => b => b.Percent.Value >= filter.MinPercent && b.Percent.Value <= filter.MaxPercent;
    
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterDateRange(this CreditBankAccountFilter filter)
        => b => b.DateRange.StartDate >= filter.StartDateRange && b.DateRange.EndDate <= filter.EndDateRange;
    
    internal static Expression<Func<CreditBankAccount, bool>> ExpressionFilterTypeCredit(this CreditBankAccountFilter filter)
        => b => b.TypeCredit.ToString() == filter.TypeCredit;
}
