using System.Linq.Expressions;

namespace _123vendas.Application.Configurations;

public static class ExpressionExtension
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.Invoke(expr1, parameter);
        var second = Expression.Invoke(expr2, parameter);

        var andExpression = Expression.AndAlso(combined, second);
        return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
    }
}
