using System;
using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public class FilterBuilder
{
    private FilterBuilder() { }

    public static Filter<T> For<T>(Expression<Func<T, bool>> expression) => new Filter<T>(expression);    
}

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


public class Filter<T>
{
    /// <summary>
    /// The predicate that holds the current expression.
    /// </summary>
    private Expression<Func<T, bool>> currentFilter;
    private Expression<Func<T, bool>> defaultExpression = e => true;

    /// <summary>
    /// Check if to see if the consumer has started the filter
    /// </summary>
    public bool HasFilter => currentFilter != null;

    internal Filter(Expression<Func<T, bool>> expression)
    {
        currentFilter = expression;
    }

    public Expression<Func<T, bool>> InitializeFilter(Expression<Func<T, bool>> filter)
    {
        if (HasFilter)
            throw new Exception("Filter can only be started once");

        return this.currentFilter = filter;
    }

    /// <summary>Or</summary>
    public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> expr2)
    {
        return (HasFilter) ? currentFilter = currentFilter.Or(expr2) : InitializeFilter(expr2);
    }

    /// <summary>And</summary>
    public Expression<Func<T, bool>> And(Expression<Func<T, bool>> expr2)
    {
        return (HasFilter) ? currentFilter = currentFilter.And(expr2) : InitializeFilter(expr2);
    }
    /// <summary>
    /// Allows this object to be implicitely converted to <see cref="Func{T, TResult}"/>
    /// </summary>
    /// <param name="right"></param>
    public static implicit operator Func<T, bool>(Filter<T> filter) => filter.currentFilter.Compile();

    /// <summary>
    /// Allows this object to be implicitely converted to an Expression{Func{T, bool}}.
    /// </summary>
    /// <param name="filter"></param>
    public static implicit operator Expression<Func<T, bool>>(Filter<T> filter)  =>  filter.currentFilter;



    

    ///// <summary>
    ///// Allows this object to be chained to other 
    //public static implicit operator Filter<T>(Expression<Func<T, bool>> filter)
    //{
    //    return filter == null ? null : new Filter<T>(right);
    //}

}

