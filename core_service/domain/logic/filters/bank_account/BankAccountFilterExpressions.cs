using System.Linq.Expressions;
using core_service.domain.models;

namespace core_service.domain.logic.filters.bank_account;

public static class BankAccountFilterExpressions
{
    internal static Expression<Func<T, bool>> ExpressionFilterName<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => e => e.Name.Value.ToLower().Contains(filter.Name!.ToLower());
    
    internal static Expression<Func<T, bool>> ExpressionFilterBalance<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => e => e.Balance.Value == filter.MinBalance!;
    internal static Expression<Func<T, bool>> ExpressionFilterBalanceRange<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => e => e.Balance.Value >= filter.MinBalance! && e.Balance.Value <= filter.MaxBalance!;
    
    internal static Expression<Func<T, bool>> ExpressionFilterCurrencyId<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => e => e.Currency.Id == filter.CurrencyId;
    
    internal static Expression<Func<T, bool>> ExpressionFilterTypeBankAccount<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => e => e.Type.ToString() == filter.TypeBankAccount;
}
