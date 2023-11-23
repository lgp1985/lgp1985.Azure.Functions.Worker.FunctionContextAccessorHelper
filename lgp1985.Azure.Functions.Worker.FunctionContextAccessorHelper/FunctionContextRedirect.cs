namespace lgp1985.Azure.Functions.Worker.FunctionContextAccessorHelper;

internal sealed class FunctionContextRedirect
{
#if NET6_0 || NET7_0 || NETSTANDARD2_0_OR_GREATER
    public FunctionContext? HeldContext;
#elif NET8_0_OR_GREATER
    public required FunctionContext HeldContext;
#endif
}
