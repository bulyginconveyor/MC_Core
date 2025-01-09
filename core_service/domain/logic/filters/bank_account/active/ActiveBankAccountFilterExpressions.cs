using System.Linq.Expressions;
using core_service.domain.models;

namespace core_service.domain.logic.filters.bank_account.active;

public static class ActiveBankAccountFilterExpressions
{
    internal static Expression<Func<ActiveBankAccount, bool>> ExpressionFilterBuyPrice(this ActiveBankAccountFilter filter)
        => b => b.BuyPrice.Value == filter.MinBuyPrice;
    internal static Expression<Func<ActiveBankAccount, bool>> ExpressionFilterBuyPriceRange(this ActiveBankAccountFilter filter)
        => b => b.BuyPrice.Value >= filter.MinBuyPrice && b.BuyPrice.Value <= filter.MaxBuyPrice;
    
    internal static Expression<Func<ActiveBankAccount, bool>> ExpressionFilterDate(this ActiveBankAccountFilter filter)
        => o => o.BuyDate == filter.StartBuyDate;
    internal static Expression<Func<ActiveBankAccount, bool>> ExpressionFilterDateRange(this ActiveBankAccountFilter filter)
        => o => o.BuyDate >= filter.StartBuyDate && o.BuyDate <= filter.EndBuyDate;
    
    internal static Expression<Func<ActiveBankAccount, bool>> ExpressionFilterTypeActive(this ActiveBankAccountFilter filter)
        => b => b.TypeActive.ToString() == filter.TypeActive;
}
