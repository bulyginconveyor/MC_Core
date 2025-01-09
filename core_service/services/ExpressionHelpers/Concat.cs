using System.Linq.Expressions;

namespace core_service.services.ExpressionHelpers;

public static class Concat
{
    public static Expression<Func<T, bool>> ExpressionСoncatWithOr<T>(this Expression<Func<T, bool>>? expression, 
        Expression<Func<T, bool>> filter)
        => expression == null
            ? filter
            : Expression.Lambda<Func<T, bool>>(Expression.Or(expression.Body, Expression.Invoke(filter, expression.Parameters)), expression.Parameters);
    
    public static Expression<Func<T, bool>> ExpressionСoncatWithAnd<T>(this Expression<Func<T, bool>>? expression, 
        Expression<Func<T, bool>> filter)
        => expression == null
            ? filter
            : Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, Expression.Invoke(filter, expression.Parameters)), expression.Parameters);
}
