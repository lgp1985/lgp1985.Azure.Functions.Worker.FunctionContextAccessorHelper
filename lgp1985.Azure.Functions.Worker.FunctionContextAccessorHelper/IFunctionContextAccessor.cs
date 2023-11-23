namespace lgp1985.Azure.Functions.Worker.FunctionContextAccessorHelper;

/// <summary>
/// Provides access to the current <see cref="FunctionContext"/>.
/// </summary>
public interface IFunctionContextAccessor
{
    /// <summary>
    /// Gets or sets the current <see cref="FunctionContext"/>.
    /// </summary>
    FunctionContext FunctionContext { get; set; }
}