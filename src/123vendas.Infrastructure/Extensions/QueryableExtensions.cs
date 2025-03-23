using System.Linq.Expressions;
using System.Reflection;

namespace _123vendas.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, string orderByClause)
    {
        if (string.IsNullOrWhiteSpace(orderByClause))
            return query;

        var orderings = orderByClause.Split(',')
                                     .Select(x => x.Trim())
                                     .Where(x => !string.IsNullOrEmpty(x))
                                     .ToList();

        bool firstOrdering = true;
        foreach (var order in orderings)
        {
            var parts = order.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
                continue;

            string propertyName = parts[0].Trim();
            bool descending = parts.Length > 1 && parts[1].Trim().Equals("desc", StringComparison.OrdinalIgnoreCase);

            PropertyInfo? property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property is null)
                continue;

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName;
            if (firstOrdering)
            {
                methodName = descending ? "OrderByDescending" : "OrderBy";
                firstOrdering = false;
            }
            else
            {
                methodName = descending ? "ThenByDescending" : "ThenBy";
            }

            var resultExpression = Expression.Call(typeof(Queryable),
                                                     methodName,
                                                     [typeof(T), property.PropertyType],
                                                     query.Expression,
                                                     Expression.Quote(orderByExpression));

            query = query.Provider.CreateQuery<T>(resultExpression);
        }

        return query;
    }
}