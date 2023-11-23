
using lgp1985.Azure.Functions.Worker.FunctionContextAccessorHelper;

namespace lgp1985.Azure.Functions.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for configuring FunctionContext services.
/// </summary>
public static class HttpServiceCollectionExtensions
{
    /// <summary>
    /// Adds a default implementation for the <see cref="IFunctionContextAccessor"/> service.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddHttpContextAccessor(this IServiceCollection services)
    {
#if NET6_0 || NET7_0 || NETSTANDARD2_0_OR_GREATER
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
#elif NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
#endif

        services.TryAddSingleton<IFunctionContextAccessor, FunctionContextAccessor>();
        return services;
    }

    /// <summary>
    /// Adds a default implementation for the <see cref="FunctionContextAccessorMiddleware"/> service.
    /// </summary>
    /// <param name="workerApplication">The <see cref="IFunctionsWorkerApplicationBuilder"/> instance this method extends.</param>
    /// <returns>The <see cref="IFunctionsWorkerApplicationBuilder"/> instance this method extends.</returns>
    public static IFunctionsWorkerApplicationBuilder UseFunctionContextAccessor(this IFunctionsWorkerApplicationBuilder workerApplication)
    {
#if NET6_0 || NET7_0 || NETSTANDARD2_0_OR_GREATER
        if (workerApplication is null)
        {
            throw new ArgumentNullException(nameof(workerApplication));
        }
#elif NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(workerApplication);
#endif

        return workerApplication.UseMiddleware<FunctionContextAccessorMiddleware>();
    }

    /// <summary>
    /// Adds a default implementation for the <see cref="IFunctionContextAccessor"/> service and the <see cref="FunctionContextAccessorMiddleware"/> service.
    /// </summary>
    /// <param name="builder">The <see cref="IHostBuilder"/> instance this method extends.</param>
    /// <returns>The <see cref="IHostBuilder"/> instance this method extends.</returns>
    public static IHostBuilder UseFunctionContextAccessor(this IHostBuilder builder)
    {
#if NET6_0 || NET7_0 || NETSTANDARD2_0_OR_GREATER
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
#elif NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(builder);
#endif

        return builder
            .ConfigureServices(services =>
            {
                services.AddHttpContextAccessor();
            })
            .ConfigureFunctionsWorkerDefaults(workerApplication =>
            {
                workerApplication.UseFunctionContextAccessor();
            });
    }
}
