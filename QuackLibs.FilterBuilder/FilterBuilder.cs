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
    /// <returns></returns>
    public static Filter<T> For<T>() => new(true);


    /// <summary>
    /// Extend an existing <see cref="{Expression{Func{T, bool}}""/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="existingFilter"></param>
    /// <returns></returns>
    public static Filter<T> Extend<T>(Expression<Func<T, bool>> existingFilter) => new(existingFilter);


    /// <summary>
    /// Extend an existing <see cref="Filter{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="existingFilter"></param>
    /// <remarks> extend filter for Filter<T>, provides similars functionality as Extend(Expression<Func<typeparamref name="</Func>)</<remarks>
    public static Filter<T> Extend<T>(Filter<T> existingFilter) => new(existingFilter);

}

