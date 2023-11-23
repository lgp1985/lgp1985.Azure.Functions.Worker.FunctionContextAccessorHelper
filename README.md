# lgp1985.FunctionContextAccessor Dependency Injection of FunctionContext
This allows usage of `FunctionContext` in a dependency injection scenario. It is intended to be used in a serverless environment where each function is executed in [isolated worker process](https://learn.microsoft.com/azure/azure-functions/dotnet-isolated-process-guide).

## Usage
On your `Program.cs` file, add the following code:
```csharp
// ...
using lgp1985.Azure.Functions.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(
        // ⬇️ Add this ⬇️
        s => s.UseFunctionContextAccessor()
        // ⬆️ Add this ⬆️
    ).ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHealthChecks();
        // ...

        // ⬇️ Add this ⬇️
        services.AddFunctionContextAccessor()
        // ⬆️ Add this ⬆️
    })
    .Build();

host.Run();
```

And then you can use in your functions services like this:
```csharp
public class TokenDelegatingHandler : DelegatingHandler
{
    private readonly IFunctionContextAccessor accessor;

    public TokenDelegatingHandler(IFunctionContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (accessor.FunctionContext is not null && request.RequestUri.Segments.LastOrDefault() != "health")
        {
            var accessToken = accessor.FunctionContext.BindingContext.BindingData.FindInRequest(HttpRequestQueries.GetAuthorizationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue(accessToken);
        }

        // ...

        return base.SendAsync(request, cancellationToken);
    }
}
```

## Remarks
This interface should be used with caution. It relies on `AsyncLocal<T>` which can have a negative performance impact on async calls. It also creates a dependency on "ambient state" which can make testing more difficult.

## See Also
This have a similar use case as [IHttpContextAccessor Interface](https://learn.microsoft.com/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor), but for the isolated model you have to use `FunctionContext` instead.

### Thanks
It was based on [benrobot/Functions.Worker.ContextAccessor](https://github.com/benrobot/Functions.Worker.ContextAccessor).