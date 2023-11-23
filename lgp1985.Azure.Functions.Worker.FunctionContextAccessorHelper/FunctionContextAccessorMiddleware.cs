

namespace lgp1985.Azure.Functions.Worker.FunctionContextAccessorHelper;

/// <inheritdoc/>
public class FunctionContextAccessorMiddleware(IFunctionContextAccessor accessor) : IFunctionsWorkerMiddleware
{
    /// <inheritdoc/>
    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
#if NET6_0 || NET7_0 || NETSTANDARD2_0_OR_GREATER
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (next is null)
        {
            throw new ArgumentNullException(nameof(next));
        }
#elif NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);
#endif

        accessor.FunctionContext = context;

        return next(context);
    }
}