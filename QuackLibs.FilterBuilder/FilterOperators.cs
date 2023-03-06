using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public static class FilterOperators
{
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> currentFilter, Expression<Func<T, bool>> additionalFilter)
    {
        var expr2Body = new RebindParameterVisitor(additionalFilter.Parameters[0], currentFilter.Parameters[0]).Visit(additionalFilter.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(currentFilter.Body, expr2Body), currentFilter.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> currentFilter, Expression<Func<T, bool>> additionalFilter)
    {
        var expr2Body = new RebindParameterVisitor(additionalFilter.Parameters[0], currentFilter.Parameters[0]).Visit(additionalFilter.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(currentFilter.Body, expr2Body), currentFilter.Parameters);
    }

    public static Expression<Func<T, bool>> Coalesce<T>(this Expression<Func<T, bool>> currentFilter, Expression<Func<T, bool>> additionalFilter)
    {
        var expr2Body = new RebindParameterVisitor(additionalFilter.Parameters[0], currentFilter.Parameters[0]).Visit(additionalFilter.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.Coalesce(currentFilter.Body, expr2Body), currentFilter.Parameters);
    }
}
