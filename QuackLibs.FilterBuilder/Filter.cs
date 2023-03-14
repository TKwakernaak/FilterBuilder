using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public class Filter<T>
{
    /// <summary>
    /// The predicate that holds the current expression.
    /// </summary>
    public Expression<Func<T, bool>> _currentFilter;

    /// <summary>
    /// The default expression created on object construction. can be invoked when no additional filters are chained
    /// </summary>
    private readonly Expression<Func<T, bool>> _defaultFilter;

    /// <summary>
    /// Check to see if the consumer has started the filter
    /// </summary>
    private bool HasFilter => (_currentFilter != null) && (_currentFilter != _defaultFilter);

    internal Filter(bool defaultFilter)
    {
        _defaultFilter = e => defaultFilter;
    }

    internal Filter(Expression<Func<T, bool>> defaultFilter) : this(false) 
    {
        _currentFilter = defaultFilter;
    }

    private Expression<Func<T, bool>> InitializeFilter(Expression<Func<T, bool>> filter)
    {
        if (HasFilter)
            throw new Exception("Filter cannot be started more than once");

        return _currentFilter = filter;
    }

    /// <summary>Or</summary>
    public Filter<T> Or(Expression<Func<T, bool>> expr2)
    {
        if (HasFilter)
            _currentFilter = _currentFilter.Or(expr2);
        else
            InitializeFilter(expr2);

        return this;
    }

    /// <summary>And</summary>
    public Filter<T> And(Expression<Func<T, bool>> expr2)
    {
        if (HasFilter)
            _currentFilter = _currentFilter.And(expr2);
        else
            InitializeFilter(expr2);

        return this;
    }

    public OptionalFilter<T> When(Func<bool> condition)
    {
        return new OptionalFilter<T>(this, condition);
    }

    /// <summary>
    /// Allows this object to be implicitely converted to <see cref="Func{T, TResult}"/>
    /// </summary>
    /// <param name="right"></param>
    public static implicit operator Func<T, bool>(Filter<T> filter) => filter._currentFilter.Compile();

    /// <summary>
    /// Allows this object to be implicitely converted to an <see cref="{Expression{Func{T, bool}}"/>.
    /// </summary>
    /// <param name="filter"></param>
    public static implicit operator Expression<Func<T, bool>>(Filter<T> filter) => filter._currentFilter;
}
