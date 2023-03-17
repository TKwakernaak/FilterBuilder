using QuackLibs.FilterBuilder;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Add the filter builder to the DI container
/// </summary>
public static class FilterBuilderServiceExtensions
{
    /// <summary>
    /// make easily extendable filter available in your application
    /// </summary>
    /// <param name="services">The services collection being extended.</param>
    public static IServiceCollection AddFilterBuilder(this IServiceCollection services)
    {
        services.AddScoped<IFilter, FilterBuilder>();
        return services;
    }
}