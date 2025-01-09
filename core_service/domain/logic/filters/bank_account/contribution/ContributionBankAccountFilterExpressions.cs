using System.Linq.Expressions;
using core_service.domain.logic.filters.bank_account.active;
using core_service.domain.models;

namespace core_service.domain.logic.filters.bank_account.contribution;

public static class ContributionBankAccountFilterExpressions
{
    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterAmount(this ContributionBankAccountFilter filter)
        => b => b.Amount.Value == filter.MinAmount;
    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterAmountRange(this ContributionBankAccountFilter filter)
        => b => b.Amount.Value >= filter.MinAmount && b.Amount.Value <= filter.MaxAmount;
    
    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterDateRange(this ContributionBankAccountFilter filter)
        => o => o.DateRange.StartDate >= filter.StartDateRange && o.DateRange.EndDate <= filter.EndDateRange &&
                (o.ActualСlosed == null || (o.ActualСlosed >= filter.StartDateRange && o.ActualСlosed <= filter.EndDateRange));
    
    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterTypeContribution(this ContributionBankAccountFilter filter)
        => b => b.TypeContribution.ToString() == filter.TypeContribution;

    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterPercent(
        this ContributionBankAccountFilter filter)
        => b => b.Percent.Percent.Value == filter.MinPercent;
    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterPercentRange(
        this ContributionBankAccountFilter filter)
        => b => b.Percent.Percent.Value >= filter.MinPercent && b.Percent.Percent.Value <= filter.MaxPercent;

    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterCountDaysForPercent(
        this ContributionBankAccountFilter filter)
        => b => b.Percent.CountDays >= filter.MinCountDaysForPercent;
    internal static Expression<Func<ContributionBankAccount, bool>> ExpressionFilterCountDaysForPercentRange(this ContributionBankAccountFilter filter)
        => b => b.Percent.CountDays >= filter.MinCountDaysForPercent && b.Percent.CountDays <= filter.MaxCountDaysForPercent;
    
}


