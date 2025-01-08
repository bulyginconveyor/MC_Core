using System.Linq.Expressions;
using core_service.domain.models;

namespace core_service.domain.logic.filters.bank_account;

public static class BankAccountFilterExpressions
{
    internal static Expression<Func<T, bool>> ExpressionFilterName<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => b => b.Name.Contains(filter.Name!);
    
    internal static Expression<Func<T, bool>> ExpressionFilterBalance<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => b => b.Balance == filter.MinBalance;
    internal static Expression<Func<T, bool>> ExpressionFilterBalanceRange<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => b => b.Balance >= filter.MinBalance && b.Balance <= filter.MaxBalance;
    
    internal static Expression<Func<T, bool>> ExpressionFilterCurrencyId<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => b => b.Currency.Id == filter.CurrencyId;
    
    internal static Expression<Func<T, bool>> ExpressionFilterTypeBankAccount<T>(this BankAccountFilter<T> filter) where T : BankAccount
        => b => b.Type.ToString() == filter.TypeBankAccount;
}
