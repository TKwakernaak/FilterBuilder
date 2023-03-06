using System;
using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public class FilterBuilder
{
    private FilterBuilder() { }

    public static Filter<T> For<T>(Expression<Func<T, bool>> expression) => new(expression);    
}

