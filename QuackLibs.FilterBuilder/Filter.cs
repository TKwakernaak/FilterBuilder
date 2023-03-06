using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public class Filter<T>
{
    /// <summary>
    /// The predicate that holds the current expression.
    /// </summary>
    private Expression<Func<T, bool>> _currentFilter;

    /// <summary>
    /// Check if to see if the consumer has started the filter
    /// </summary>
    public bool HasFilter => _currentFilter != null;

    internal Filter(Expression<Func<T, bool>> expression)
    {
        _currentFilter = expression;
    }

    public Expression<Func<T, bool>> InitializeFilter(Expression<Func<T, bool>> filter)
    {
        if (HasFilter)
            throw new Exception("Filter can only be started once");

        return this._currentFilter = filter;
    }

    /// <summary>Or</summary>
    public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> expr2)
    {
        return (HasFilter) ? _currentFilter = _currentFilter.Or(expr2) : InitializeFilter(expr2);
    }

    /// <summary>And</summary>
    public Expression<Func<T, bool>> And(Expression<Func<T, bool>> expr2)
    {
        return (HasFilter) ? _currentFilter = _currentFilter.And(expr2) : InitializeFilter(expr2);
    }
    /// <summary>
    /// Allows this object to be implicitely converted to <see cref="Func{T, TResult}"/>
    /// </summary>
    /// <param name="right"></param>
    public static implicit operator Func<T, bool>(Filter<T> filter) => filter._currentFilter.Compile();

    /// <summary>
    /// Allows this object to be implicitely converted to an Expression{Func{T, bool}}.
    /// </summary>
    /// <param name="filter"></param>
    public static implicit operator Expression<Func<T, bool>>(Filter<T> filter) => filter._currentFilter;
}
