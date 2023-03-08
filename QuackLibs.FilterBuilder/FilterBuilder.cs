using System;
using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder;

public class FilterBuilder
{
    private FilterBuilder() { }

    /// <summary>
    /// Start a new filter for <see cref="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="defaultFilter"></param>
    /// <returns></returns>
    public static Filter<T> For<T>(bool defaultFilter) => new(defaultFilter);


    /// <summary>
    /// Extend an existing <see cref="Filter{T}"/> or <see cref="{Expression{Func{T, bool}}""/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="defaultFilter"></param>
    /// <returns></returns>
    public static Filter<T> Extend<T>(Expression<Func<T, bool>> existingFilter) => new(existingFilter);

}

