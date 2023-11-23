namespace lgp1985.Azure.Functions.Worker.FunctionContextAccessorHelper;

/// <inheritdoc/>
public class FunctionContextAccessor : IFunctionContextAccessor
{
    private static readonly AsyncLocal<FunctionContextRedirect> currentContext = new();

    /// <inheritdoc/>
    public virtual FunctionContext FunctionContext
    {
        get
        {
            return currentContext.Value?.HeldContext!;
        }

        set
        {
            var holder = currentContext.Value;
            if (holder != null)
            {
                // Clear current context trapped in the AsyncLocals, as its done.
                holder.HeldContext = null;
            }

            if (value != null)
            {
                // Use an object indirection to hold the context in the AsyncLocal,
                // so it can be cleared in all ExecutionContexts when its cleared.
                currentContext.Value = new FunctionContextRedirect { HeldContext = value };
            }
        }
    }
}