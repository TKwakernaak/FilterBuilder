using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public class OptionalFilter<T>
{
    private readonly Func<bool> _condition;
    private Filter<T> _filter { get; set; }

    public OptionalFilter(Filter<T> filter, Func<bool> condition)
    {
        _filter = filter;
        _condition = condition;
    }

    public Filter<T> Then(Expression<Func<T, bool>> expr2)
    {
        if (_condition())
            _filter.And(expr2);

        return _filter;
    }
}
